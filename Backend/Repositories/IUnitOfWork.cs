using Backend.Models.Products;
using Backend.Repositories.Product;

namespace Backend.Repositories
{
    public interface IUnitOfWork
    {
        IRepository<BrandModel> BrandRepository { get; }
        IRepository<Category> CategoryRepository { get; }
        IRepository<ProductModel> ProductRepository { get; }
        IRepository<Media> MediaRepository { get; }
        Task CompleteAsync();
        
    }
}
