using Backend.Models.Identity;
using gearshop_dotnetapp.Data;
using System.Linq.Expressions;

namespace gearshop_dotnetapp.Repositories
{
    public class AddressRepository : GenericRepository<Address>
    {
        public AddressRepository(DataContext context) : base(context)
        {
        }

        public override IEnumerable<Address> Find(Expression<Func<Address, bool>> predicate)
        {
            return base.Find(predicate);
        }
    }
}
