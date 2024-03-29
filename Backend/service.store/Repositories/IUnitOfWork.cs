﻿using App.Models.Identity;
using App.Models.OrderModel;
using App.Models.ProductModel;

namespace App.Repositories
{
      public interface IUnitOfWork
      {
            IRepository<Product> ProductRepository { get; }
            IRepository<Category> CategoryRepository { get; }
            IRepository<Order> OrderRepository { get; }
            IRepository<OrderItem> OrderItemRepository { get; }
            IRepository<Cart> CartRepository { get; }
            IRepository<Address> AddressRepository { get; }
            IRepository<Icon> IconRepository { get; }
            IRepository<ProductCategory> ProductCategoryRepository { get; }

            Task CompleteAsync();
      }
}
