using App.Data;
using App.Models.Identity;

namespace App.Repositories
{
      public class AddressRepository : GenericRepository<Address>
      {
            public AddressRepository(DataContext context) : base(context)
            {
            }
      }
}