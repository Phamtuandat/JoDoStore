using Backend.Models.Products;
using Backend.Repositories.Product;

namespace Backend.Repositories
{
    public interface IUnitOfWork
    {
        IRepository<BrandModel> AuthorRepository { get; }
        IRepository<Category> CategoryRepository { get; }
        IRepository<Models.Products.ProductModel> BookRepository { get; }
        Task CompleteAsync();
        
    }
}
