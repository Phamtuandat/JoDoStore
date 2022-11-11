using Backend.Models.Products;
using Backend.Repositories;
using Backend.Resources;

namespace Backend.Services.Product
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private static IWebHostEnvironment? _webHostEnvironment;
        public ProductService(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<ProductRes> CreateAsync(SaveProductResource model)
        {
            var existedProduct =  _unitOfWork.ProductRepository.Find(b => b.Name == model.Name)?.FirstOrDefault();
            if (existedProduct != null) return new ProductRes("The book is existed!");
            try
            { 
                var brand = new BrandModel();
                var brandExisted = _unitOfWork.BrandRepository.All().FirstOrDefault(b => b.Id == model.Brand);
                
                if(brandExisted != null) brand = brandExisted;
                var categoryList = new List<Category>();
                foreach (var item in model.CategoryList)
                {
                    var category = _unitOfWork.CategoryRepository.All().FirstOrDefault(a => a.Id == item);
                    if(category != null) categoryList.Add(category);
                }
                var media = new List<Media>();
                if(model.Media.Count > 0)
                {
                    if (!Directory.Exists(_webHostEnvironment.WebRootPath + "\\Images\\"))
                    {
                        Directory.CreateDirectory(_webHostEnvironment.WebRootPath + "\\Images\\");
                    }
                    string webRootPath = _webHostEnvironment.WebRootPath;
                    var files = model.Media;
                    foreach (var item in files)
                    {
                        var uploads = Path.Combine(webRootPath, "images");
                        var extension = Path.GetExtension(item.FileName);
                        var dynamicFileName =  Guid.NewGuid().ToString() + "_" + model.Name.Replace(" ", "") + extension;

                        using (var filesStream = new FileStream(Path.Combine(uploads, dynamicFileName), FileMode.Create))
                        {
                            item.CopyTo(filesStream);
                        }
                        Media newMedia = new Media() { thumbnailPath = dynamicFileName, Title = model.Name };
                        _unitOfWork.MediaRepository.Add(newMedia);
                        //add product Image for new product
                        media.Add(newMedia);
                    }
                }
                var newBook = new ProductModel()
                {
                    Name = model.Name,
                    Descriptions = model.Descriptions,
                    Price = model.Price,
                    PriceSale = model.PriceSale,
                    CreatedTimestamp = DateTime.Now,
                    Brand = brand,
                    Categories = categoryList,
                    Media = media,
                };
                var res = _unitOfWork.ProductRepository.Add(newBook);
                await _unitOfWork.CompleteAsync();
                return new ProductRes(res);

            }catch(Exception ex)
            {
                return new ProductRes($"Something went wrong when saving the book {ex.Message}, please try again later!");
            }
        }

        public async Task<ProductRes> DeletAsync(int id)
        {
            var existtingBook = _unitOfWork.ProductRepository.Get(id);
            if (existtingBook == null) return new ProductRes("The book is not already exist!");
            try
            {
                var bookUpdated = _unitOfWork.ProductRepository.Delete(existtingBook);
                await _unitOfWork.CompleteAsync();
                return new ProductRes(true);
            }
            catch (Exception ex)
            {
                return new ProductRes($"Something went wrong when deleting the book, please try again later!, {ex.Message}");
            }
        }

        public IQueryable<ProductModel> GetAll()
        {
            var books =  _unitOfWork.ProductRepository.All();
            return books;
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task<ProductRes> GetById(int id)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            var product = _unitOfWork.ProductRepository.All().FirstOrDefault(p => p.Id == id);
            if (product == null) return new ProductRes("Can't find the product");
            return new ProductRes(product);
        }

        public async Task<ProductRes> UpdateAsync(SaveProductResource model, int id)
        {
            var existedProduct = _unitOfWork.ProductRepository.All().FirstOrDefault(p => p.Id == id);
            if (existedProduct == null) return new ProductRes("The book is not already exist!");
            var categoryList = new List<Category>();
            if(model.CategoryList.Count > 0 && model.CategoryList != null)
            {
                foreach (var item in model.CategoryList)
                {
#pragma warning disable CS8604 // Possible null reference argument.
                    categoryList.Add(_unitOfWork.CategoryRepository.Find(c => c.Id == item).FirstOrDefault());
#pragma warning restore CS8604 // Possible null reference argument.
                }
            }
            try
            {
                existedProduct.Name = model.Name;
                existedProduct.Price = model.Price;
                existedProduct.Descriptions = model.Descriptions;
                existedProduct.PriceSale = model.PriceSale;
                existedProduct.Categories = categoryList;
                var bookUpdated = _unitOfWork.ProductRepository.Update(existedProduct);
                await _unitOfWork.CompleteAsync();
                return new ProductRes(bookUpdated);
            }catch(Exception ex)
            {
                return new ProductRes($"Something went wrong when updating the book, please try again later!, {ex.Message}");
            }
        }
    }
}
