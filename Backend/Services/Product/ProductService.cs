using Backend.Models.Products;
using Backend.Repositories;
using Backend.Resources;

namespace Backend.Services.Product
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ProductRes> CreateAsync(SaveProductResource model)
        {
            var existedProduct =  _unitOfWork.BookRepository.Find(b => b.Name == model.Name)?.FirstOrDefault();
            if (existedProduct != null) return new ProductRes("The book is existed!");
            try
            {
                var brandList =  new List<Models.Products.BrandModel>();
                foreach (var item in model.Brands)
                {
                    var author = _unitOfWork.AuthorRepository.Find(a => a.Id == item)?.FirstOrDefault();
                    if(author != null) brandList.Add(author);  
                }
                
                var categoryList = new List<Category>();
                foreach (var item in model.CategoryList)
                {
                    var category = _unitOfWork.CategoryRepository.Find(a => a.Id == item)?.FirstOrDefault();
                    if(category != null) categoryList.Add(category);
                }
                
                var newBook = new Models.Products.ProductModel()
                {
                    Name = model.Name,
                    Descriptions = model.Descriptions,
                    Price = model.Price,
                    PriceSale = model.PriceSale,
                    CreatedTimestamp = DateTime.Now,
                    Brands = brandList,
                    Categories = categoryList
                };
                var res = _unitOfWork.BookRepository.Add(newBook);
                await _unitOfWork.CompleteAsync();
                return new ProductRes(res);

            }catch(Exception ex)
            {
                return new ProductRes($"Something went wrong when saving the book {ex.Message}, please try again later!");
            }
        }

        public async Task<ProductRes> DeletAsync(int id)
        {
            var existtingBook = _unitOfWork.BookRepository.Get(id);
            if (existtingBook == null) return new ProductRes("The book is not already exist!");
            try
            {
                var bookUpdated = _unitOfWork.BookRepository.Delete(existtingBook);
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
            var books =  _unitOfWork.BookRepository.All();
            return books;
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task<ProductRes> GetById(int id)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            var product = _unitOfWork.BookRepository.Get(id);
            if (product == null) return new ProductRes("Can't find the book");
            return new ProductRes(product);
        }

        public async Task<ProductRes> UpdateAsync(SaveProductResource model, int id)
        {
            var existedProduct = _unitOfWork.BookRepository.Get(id);
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
                var bookUpdated = _unitOfWork.BookRepository.Update(existedProduct);
                await _unitOfWork.CompleteAsync();
                return new ProductRes(bookUpdated);
            }catch(Exception ex)
            {
                return new ProductRes($"Something went wrong when updating the book, please try again later!, {ex.Message}");
            }
        }
    }
}
