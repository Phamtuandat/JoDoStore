using gearshop_dotnetapp.Data;
using gearshop_dotnetapp.Models.OrderModel;

namespace gearshop_dotnetapp.Repositories
{
    public class OrderItemRepository : GenericRepository<OrderItem>
    {
        public OrderItemRepository(DataContext context) : base(context)
        {
        }
    }
}
