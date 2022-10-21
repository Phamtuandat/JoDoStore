using Backend.Models.Products;
using Backend.Repositories.Product;

namespace Backend.Repositories
{
    public interface IUnitOfWork
    {
        IRepository<Author> AuthorRepository { get; }
        IRepository<Category> CategoryRepository { get; }
        IRepository<Book> BookRepository { get; }
        Task CompleteAsync();
        
    }
}
