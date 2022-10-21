using Backend.Models.Products;
using Backend.Repositories;
using Backend.Repositories.Product;
using Backend.Resources;

namespace Backend.Services.Product
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;
        public BookService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BookRes> CreateAsync(SaveBookResource model)
        {
            var existedBook =  _unitOfWork.BookRepository.Find(b => b.Name == model.Name).FirstOrDefault();
            if (existedBook != null) return new BookRes("The book is existed!");
            try
            {
                var authorList =  new List<Author>();
                foreach (var item in model.Authors)
                {
                    var author = _unitOfWork.AuthorRepository.Find(a => a.Id == item).FirstOrDefault();
                    if(author != null) authorList.Add(author);  
                }
                
                var categoryList = new List<Category>();
                foreach (var item in model.CategoryList)
                {
                    var category = _unitOfWork.CategoryRepository.Find(a => a.Id == item).FirstOrDefault();
                    if(category != null) categoryList.Add(category);
                }
                
                var newBook = new Book()
                {
                    Name = model.Name,
                    Descriptions = model.Descriptions,
                    Price = model.Price,
                    PriceSale = model.PriceSale,
                    CreatedTimestamp = DateTime.Now,
                    Authors = authorList,
                    Categories = categoryList
                };
                var res = _unitOfWork.BookRepository.Add(newBook);
                await _unitOfWork.CompleteAsync();
                return new BookRes(res);

            }catch(Exception ex)
            {
                return new BookRes($"Something went wrong when saving the book {ex.Message}, please try again later!");
            }
        }

        public async Task<BookRes> DeletAsync(int id)
        {
            var existtingBook = _unitOfWork.BookRepository.Get(id);
            if (existtingBook == null) return new BookRes("The book is not already exist!");
            try
            {
                var bookUpdated = _unitOfWork.BookRepository.Delete(existtingBook);
                await _unitOfWork.CompleteAsync();
                return new BookRes(true);
            }
            catch (Exception ex)
            {
                return new BookRes($"Something went wrong when deleting the book, please try again later!, {ex.Message}");
            }
        }

        public IQueryable<Book> GetAll()
        {
            var books =  _unitOfWork.BookRepository.All();
            return books;
        }

        public async Task<BookRes> GetById(int id)
        {
            var book = _unitOfWork.BookRepository.Get(id);
            if (book == null) return new BookRes("Can't find the book");
            return new BookRes(book);
        }

        public async Task<BookRes> UpdateAsync(SaveBookResource model, int id)
        {
            var existtingBook = _unitOfWork.BookRepository.Get(id);
            if (existtingBook == null) return new BookRes("The book is not already exist!");
            var categoryList = new List<Category>();
            foreach (var item in model.CategoryList)
            {
                categoryList.Add(_unitOfWork.CategoryRepository.Find(c => c.Id == item).FirstOrDefault());
            }
            try
            {
                existtingBook.Name = model.Name;
                existtingBook.Price = model.Price;
                existtingBook.Descriptions = model.Descriptions;
                existtingBook.PriceSale = model.PriceSale;
                existtingBook.Categories = categoryList;
                var bookUpdated = _unitOfWork.BookRepository.Update(existtingBook);
                await _unitOfWork.CompleteAsync();
                return new BookRes(bookUpdated);
            }catch(Exception ex)
            {
                return new BookRes($"Something went wrong when updating the book, please try again later!, {ex.Message}");
            }
        }
    }
}
