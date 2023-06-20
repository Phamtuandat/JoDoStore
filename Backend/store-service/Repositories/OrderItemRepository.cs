using App.Data;
using App.Models.OrderModel;
using Microsoft.EntityFrameworkCore;

namespace App.Repositories
{
      public class OrderItemRepository : GenericRepository<OrderItem>
      {
            public OrderItemRepository(DataContext context) : base(context)
            {
            }
            public override IQueryable<OrderItem> All()
            {
                  return base.All();
            }
      }
}
