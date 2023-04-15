using Backend.Models.Identity;
using gearshop_dotnetapp.Data;
using System.Linq.Expressions;

namespace gearshop_dotnetapp.Repositories
{
    public class AddressRepository : GenericRepository<AddressBook>
    {
        public AddressRepository(DataContext context) : base(context)
        {
        }
    }
}
