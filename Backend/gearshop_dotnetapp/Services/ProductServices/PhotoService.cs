using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using dotenv.net;
using gearshop_dotnetapp.Models.ProductModel;
using gearshop_dotnetapp.Repositories;
using gearshop_dotnetapp.Resources;
using gearshop_dotnetapp.Services.Communications;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Text.RegularExpressions;

namespace gearshop_dotnetapp.Services.ProductServices
{
    public class PhotoService : IPhotoService
    {
        private readonly Cloudinary _cloudinary;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uniOfWork;
        private readonly ILogger<PhotoService> _logger;
        public PhotoService(IUnitOfWork uniOfWork, IMapper mapper, ILogger<PhotoService> logger)
        {
            DotEnv.Load(options: new DotEnvOptions(probeForEnv: true));
            _cloudinary = new Cloudinary(Environment.GetEnvironmentVariable("CLOUDINARY_URL"));
            _cloudinary.Api.Secure = true;
            _uniOfWork = uniOfWork;
            _mapper = mapper;
            _logger = logger;
            _logger.LogInformation("Logging!");
        }

        public async Task<PhotoResponse> CreateAsync(SavPhotoResource model)
        {
            try
            {
                var file = model.FormFile;
                if (file == null) return new PhotoResponse("form file is required!");
                var collections = _uniOfWork.ImageCollectionsRepository.Find(x => x.Name == model.Collections)?.FirstOrDefault();
                if(collections == null)
                {
                    collections = new ImageCollections() { Name = model.Collections };
                    _uniOfWork.ImageCollectionsRepository.Add(collections);
                }
                string[] resize = { model.Collections.ToLower() };
                string title = Regex.Replace(model.Title, @"[^0-9a-zA-Z:,]+", "");
                var dynamicFileName = Convert.ToBase64String(Guid.NewGuid().ToByteArray()) + "_" + title;
                using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(dynamicFileName, stream),
                    UniqueFilename = true,
                    DisplayName = title,
                    PublicId = dynamicFileName,
                    EagerTransforms = new List<Transformation>()
                    {
                        new Transformation().Named(resize)
                    }
                };
                var uploadResult = await _cloudinary.UploadAsync(uploadParams);
                var eagerList = uploadResult.Eager;
                string? imageUrl;
                if (eagerList != null && eagerList.Length > 0)
                {
                    imageUrl = eagerList.FirstOrDefault()?.SecureUrl.ToString();
                    _logger.LogInformation(imageUrl);
                }
                else
                {
                    imageUrl =  uploadResult.SecureUrl.ToString();
                }
                imageUrl ??= uploadResult.SecureUrl.ToString();

                var newThumb = new Photo()
                {
                    Title = model.Title,
                    ImageCollections = collections,
                    ImageUrl = imageUrl,
                    Description = model.Description,
                    ProductId = model.ProductId,
                    PublicId = uploadResult.PublicId,
                };
                var result = _uniOfWork.ThumbnailRepository.Add(newThumb);
                await _uniOfWork.CompleteAsync();
                return new PhotoResponse(result);

            }
            catch (Exception ex)
            {

                return new PhotoResponse($"something went wrong, Please try again later! \n message: {ex.Message}");
            }
        }

        public async Task<PhotoResponse> DeleteAsync(int id)
        {
            try
            {
                var exitedThub = _uniOfWork.ThumbnailRepository.Find(x => x.Id == id)?.FirstOrDefault();
                if (exitedThub == null) return new PhotoResponse("can not find thumbnai");
                var uploadResult = await _cloudinary.DestroyAsync(new DeletionParams(exitedThub.PublicId));
                var result = _uniOfWork.ThumbnailRepository.Delete(exitedThub);
                await _uniOfWork.CompleteAsync();

                return new PhotoResponse(result);
            }
            catch (Exception ex)
            {
                return new PhotoResponse($"Something went wrong, please try again \n message: {ex.Message}");
            }
        }

        public async Task<IEnumerable<PhotoResource>> GetAllPhoto()
        {
            var result  = await _uniOfWork.ThumbnailRepository.All().ToListAsync();
            return _mapper.Map<IEnumerable<Photo>, IEnumerable<PhotoResource>>(result);
        }

        public PhotoResponse GetPhotoById(int id)
        {
            var thumb = _uniOfWork.ThumbnailRepository.Get(id);
            if (thumb != null) return new PhotoResponse(thumb);
            return new PhotoResponse("can not find thumbnai");
        }

        public async Task<PhotoResponse> UpdateAsync(SavPhotoResource model, int id)
        {
            var thumb = _uniOfWork.ThumbnailRepository.Get(id);
            if(thumb != null)
            {
                try
                {
                    var file = model.FormFile;
                    if (file != null)
                    {
                        var extension = Path.GetExtension(file.FileName);
                        string title = Regex.Replace(model.Title, @"[^0-9a-zA-Z:,]+", "");
                        var dynamicFileName = Convert.ToBase64String(Guid.NewGuid().ToByteArray()) + "_" + title + extension;
                        using var stream = file.OpenReadStream();
                        var uploadParams = new ImageUploadParams()
                        {
                            File = new FileDescription(dynamicFileName, stream),
                            PublicId = thumb.PublicId
                        };
                        var uploadResult = _cloudinary.Upload(uploadParams);

                        thumb.ImageUrl = uploadResult.Url.ToString();
                    }
                    if (model.Collections != thumb.ImageCollections?.Name)
                    {
                        var collections = _uniOfWork.ImageCollectionsRepository.Find(x => x.Name == model.Collections)?.FirstOrDefault();
                        if (collections == null)
                        {
                            collections = new ImageCollections() { Name = model.Collections };
                            _uniOfWork.ImageCollectionsRepository.Add(collections);
                        };
                        thumb.ImageCollections = collections;
                    }

                    thumb.Description = model.Description;
                    thumb.Title = model.Title;
                    var result = _uniOfWork.ThumbnailRepository.Update(thumb);
                    await _uniOfWork.CompleteAsync();
                    return new PhotoResponse(result);
                }
                catch (Exception ex)
                {

                    return new PhotoResponse($"Something went wrong, \n message:  {ex.Message}");
                }

            }
           return new PhotoResponse("Id is invalid!");
        }
    }
}
