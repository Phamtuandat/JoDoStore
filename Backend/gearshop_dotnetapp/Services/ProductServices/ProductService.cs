using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using dotenv.net;
using gearshop_dotnetapp.Models.Product;
using gearshop_dotnetapp.Repositories;
using gearshop_dotnetapp.Resources;
using gearshop_dotnetapp.Services.Communications;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace gearshop_dotnetapp.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly Cloudinary _cloudinary;
        public ProductService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            DotEnv.Load(options: new DotEnvOptions(probeForEnv: true));
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cloudinary = new Cloudinary(Environment.GetEnvironmentVariable("CLOUDINARY_URL"));
            _cloudinary.Api.Secure = true;
        }
        public async Task<ProductRes> CreateAsync(SaveProductResource saveProductResource)
        {
            var existedProduct = _unitOfWork.ProductRepository.Find(p => p.Name == saveProductResource.Name)?.FirstOrDefault();
            if(existedProduct != null) return new ProductRes("Product name has already existed");

            

            try
            {

                var category = _unitOfWork.CategoryRepository.Find(c => c.Name == saveProductResource.Category.Name)?.FirstOrDefault();
                if (category == null)
                {
                    category = new Category() { Name = saveProductResource.Category.Name };
                    _unitOfWork.CategoryRepository.Add(category);
                }

                var brand = _unitOfWork.BrandRepository.Find(b => b.Name == saveProductResource.Brand.Name)?.FirstOrDefault();
                if(brand == null)
                {
                    brand = new Brand() { Name = saveProductResource.Brand.Name };
                    _unitOfWork.BrandRepository.Add(brand);
                }

                var files = saveProductResource.Thumbnails;
                if (files == null) return new ProductRes("formfile is Required!");

                var collection = _unitOfWork.ImageCollectionsRepository.Find(c => c.Name == "Product Image")?.FirstOrDefault();
                if (collection == null)
                {
                    collection = new ImageCollections() { Name = "Product Image" };
                    _unitOfWork.ImageCollectionsRepository.Add(collection);
                }
                var thumbnails = new List<Photo>();
                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {
                        var extension = Path.GetExtension(file.FileName);
                        string title = Regex.Replace(saveProductResource.Name, @"[^0-9a-zA-Z:,]+", "");
                        var dynamicFileName = Convert.ToBase64String(Guid.NewGuid().ToByteArray()) + "_" + title + extension;
                        using var stream = file.OpenReadStream();
                        var uploadParams = new ImageUploadParams()
                        {
                            File = new FileDescription(dynamicFileName,stream)
                        };
                        var uploadResult = _cloudinary.Upload(uploadParams);

                        var newThumbnail = new Photo()
                        {
                            ImageUrl = uploadResult.Url.ToString(),
                            PublicId = uploadResult.PublicId,
                            ImageCollections = collection,
                            Created = DateTime.UtcNow,
                            Title = title,

                        };
                        thumbnails.Add(newThumbnail);
                    }
                }

                var tags = new List<Tag>();
                if(saveProductResource.Tags != null)
                {
                    foreach (var item in saveProductResource.Tags)
                    {
                        var tag = _unitOfWork.TagRepository.Find(x => x.Name == item)?.FirstOrDefault();
                        if(tag == null)
                        {
                           tag = new Tag() { Name = item };
                           _unitOfWork.TagRepository.Add(tag);
                        }
                        tags.Add(tag);
                    }
                }

                var newProduct = new Product()
                {
                    Name = saveProductResource.Name,
                    Brand = brand,
                    Category = category,
                    Description = saveProductResource.Description,
                    Tags = tags,
                    Thumbnails = thumbnails,
                    NormalizedName = saveProductResource.Name.ToUpper(),
                    CreateAt= DateTime.UtcNow,
                    SalePrice= saveProductResource.SalePrice,
                    Price = saveProductResource.Price,
                    
                };
                var product = _unitOfWork.ProductRepository.Add(newProduct);
                await _unitOfWork.CompleteAsync();
                var result = _mapper.Map<Product, ProductResource>(product);
                return new ProductRes(result);

            }
            catch (Exception ex)
            {
                return new ProductRes($"Something went wrong when saving product, \n message: {ex.Message}");
            }
            
        }

        public async Task<ProductRes> DeleteAsync(int id)
        {
            var product = _unitOfWork.ProductRepository.Get(id);
            if (product == null) return new ProductRes("Can not find product id!");
            try
            {
                var thumbList = product.Thumbnails.ToList();
                foreach (var thumb in thumbList)
                {
                    var uploadResult = await _cloudinary.DeleteResourcesAsync(thumb.PublicId);
                }
                _unitOfWork.ProductRepository.Delete(product);
                await _unitOfWork.CompleteAsync();
                return new ProductRes(true);
            }
            catch (Exception ex)
            {

                return new ProductRes($"Something went wrong when deleting the product, \n message: {ex.Message}");
            }
        }

        public async Task<IEnumerable<ProductResource>> GetAll()
        {
            var products = await _unitOfWork.ProductRepository.All().ToListAsync();
            var res = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductResource>>(products);
            return res;
        }
         
        public ProductRes GetById(int id)
        {
            var product = _unitOfWork.ProductRepository.All().FirstOrDefault(x=> x.Id == id);

            if (product != null) {
                var productResource = _mapper.Map<Product, ProductResource>(product);
                return new ProductRes(productResource);
            };
            return new ProductRes("Can not find product");
        }

        public IEnumerable<ProductResource> FindByName(string name)
        {
            var products =  _unitOfWork.ProductRepository.Find(x => x.NormalizedName.Contains(name.ToUpper()))?.ToList();
            if (products == null) return new List<ProductResource>();
            var result = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductResource>>(products);
            return result;
        }

        public async Task<ProductRes> UpdateAsync(SaveProductResource saveProductResource, int id)
        {
            var existedProduct = _unitOfWork.ProductRepository.Get(id);
            if (existedProduct != null)
            {
                if(saveProductResource.Brand != null && saveProductResource.Brand.Name != existedProduct.Brand?.Name)
                {
                    var updateBrand = _unitOfWork.BrandRepository.Find(x => x.Name == saveProductResource.Brand.Name)?.FirstOrDefault();
                    if(updateBrand == null)
                    {
                        updateBrand = new Brand() { Name = saveProductResource.Brand.Name };
                        _unitOfWork.BrandRepository.Add(updateBrand);
                        existedProduct.Brand = updateBrand;
                    }
                }
                if (saveProductResource.Category != null && saveProductResource.Category.Name != existedProduct.Category?.Name)
                {
                    var updateCategory = _unitOfWork.CategoryRepository.Find(x => x.Name == saveProductResource.Category.Name)?.FirstOrDefault();
                    if (updateCategory == null)
                    {
                        updateCategory = new Category() { Name = saveProductResource.Category.Name };
                        _unitOfWork.CategoryRepository.Add(updateCategory);
                        existedProduct.Category = updateCategory;
                    }
                }
                existedProduct.Name= saveProductResource.Name;
                existedProduct.Description= saveProductResource.Description;
                existedProduct.NormalizedName = saveProductResource.Name.ToUpper();
                existedProduct.SalePrice = saveProductResource.SalePrice;
                existedProduct.Price= saveProductResource.Price;

                var result = _unitOfWork.ProductRepository.Update(existedProduct);
                await _unitOfWork.CompleteAsync();
                var productResorce = _mapper.Map<Product, ProductResource>(result);
                return new ProductRes(productResorce);
            };
            return new ProductRes("can not find product!");
        }
    }
}
