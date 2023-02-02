using gearshop_dotnetapp.Models.Product;

namespace gearshop_dotnetapp.Repositories
{
    public interface IUnitOfWork
    {
        IRepository<Product> ProductRepository { get; }
        IRepository<Category> CategoryRepository { get; }
        IRepository<Photo> ThumbnailRepository { get; }
        IRepository<ImageCollections> ImageCollectionsRepository { get; }
        IRepository<Brand> BrandRepository { get; }
        IRepository<Tag> TagRepository { get; }
        Task CompleteAsync();
    }
}
