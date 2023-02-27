using gearshop_dotnetapp.Data;
using gearshop_dotnetapp.Models.OrderModel;

namespace gearshop_dotnetapp.Repositories
{
    public class OrderRepository : GenericRepository<Order>
    {
        public OrderRepository(DataContext context) : base(context)
        {   
        }
    }
}
