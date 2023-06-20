using App.Data;
using App.Models.OrderModel;
using Microsoft.EntityFrameworkCore;

namespace App.Repositories
{
      public class OrderRepository : GenericRepository<Order>
      {
            public OrderRepository(DataContext context) : base(context)
            {
            }

            public override IQueryable<Order> All()
            {
                  return base.All().Include(o => o.OrderItems).Include(o => o.Address).Include(o => o.User);
            }

      }
}
