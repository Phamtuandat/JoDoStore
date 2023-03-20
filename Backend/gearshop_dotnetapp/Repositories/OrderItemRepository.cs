using gearshop_dotnetapp.Data;
using gearshop_dotnetapp.Models.OrderModel;
using Microsoft.EntityFrameworkCore;

namespace gearshop_dotnetapp.Repositories
{
    public class OrderItemRepository : GenericRepository<OrderItem>
    {
        public OrderItemRepository(DataContext context) : base(context)
        {
        }
        public override IQueryable<OrderItem> All()
        {
            return base.All().Include(o => o.Product);
        }
    }
}
