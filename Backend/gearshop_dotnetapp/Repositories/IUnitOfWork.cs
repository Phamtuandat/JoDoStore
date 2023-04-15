using Backend.Models.Identity;
using gearshop_dotnetapp.Models.OrderModel;
using gearshop_dotnetapp.Models.ProductModel;

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
        IRepository<Order> OrderRepository { get; }
        IRepository<OrderItem> OrderItemRepository { get; }
        IRepository<AddressBook> AddressRepository { get; }
        IRepository<Cart> CartRepository { get; }
        Task CompleteAsync();
    }
}
