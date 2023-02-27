using Backend.Models.Identity;
using gearshop_dotnetapp.Data;

namespace gearshop_dotnetapp.Repositories
{
    public class AdressRepository : GenericRepository<Address>
    {
        public AdressRepository(DataContext context) : base(context)
        {
        }
    }
}
