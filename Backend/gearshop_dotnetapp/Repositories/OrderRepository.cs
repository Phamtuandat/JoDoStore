using gearshop_dotnetapp.Data;
using gearshop_dotnetapp.Models.OrderModel;
using Microsoft.EntityFrameworkCore;

namespace gearshop_dotnetapp.Repositories
{
    public class OrderRepository : GenericRepository<Order>
    {
        public OrderRepository(DataContext context) : base(context)
        {   
        }

        public override IQueryable<Order> All()
        {
            return base.All().Include(o => o.OrderItems).Include(o => o.AddressBook).Include(o => o.User);
        }

    }
}
