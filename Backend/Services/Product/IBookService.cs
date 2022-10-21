using Backend.Models.Products;
using Backend.Resources;

namespace Backend.Services.Product
{
    public interface IBookService
    {
        IQueryable<Book> GetAll();
        Task<BookRes> CreateAsync(SaveBookResource model);
        Task<BookRes> UpdateAsync(SaveBookResource book, int id);
        Task<BookRes> DeletAsync(int id);
        Task<BookRes> GetById(int id);
    }
}
