using App.Data;
using App.Models.Identity;
using App.Models.OrderModel;
using App.Models.ProductModel;

namespace App.Repositories
{
      public class UnitOfWork : IUnitOfWork
      {
            private readonly DataContext _context;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
            public UnitOfWork(DataContext context)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
            {
                  _context = context;
            }

            private IRepository<Category> categoryRepository;
            public IRepository<Category> CategoryRepository
            {
                  get
                  {
                        categoryRepository ??= new CategoryRepository(_context);
                        return categoryRepository;
                  }
            }

            private IRepository<Product> productRepository;
            public IRepository<Product> ProductRepository
            {
                  get
                  {
                        productRepository ??= new ProductRepository(_context);
                        return productRepository;
                  }
            }


            private IRepository<Brand> brandRepository;
            public IRepository<Brand> BrandRepository
            {
                  get
                  {
                        brandRepository ??= new BrandRepository(_context);
                        return brandRepository;
                  }
            }

            private IRepository<Address> addressRepository;
            public IRepository<Address> AddressRepository
            {
                  get
                  {
                        addressRepository ??= new AddressRepository(_context);
                        return addressRepository;
                  }
            }


            private IRepository<Order> orderRepository;
            public IRepository<Order> OrderRepository
            {
                  get
                  {
                        orderRepository ??= new OrderRepository(_context);
                        return orderRepository;
                  }
            }

            private IRepository<OrderItem> orderItemRepository;
            public IRepository<OrderItem> OrderItemRepository
            {
                  get
                  {
                        orderItemRepository ??= new OrderItemRepository(_context);
                        return orderItemRepository;
                  }
            }
            private IRepository<Cart> cartRepository;
            public IRepository<Cart> CartRepository
            {
                  get
                  {
                        cartRepository ??= new CartRepository(_context);
                        return cartRepository;
                  }
            }

            public async Task CompleteAsync()
            {
                  await _context.SaveChangesAsync();
            }

      }
}
