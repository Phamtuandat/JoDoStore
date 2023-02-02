using gearshop_dotnetapp.Data;
using gearshop_dotnetapp.Models.Product;

namespace gearshop_dotnetapp.Repositories
{
    public class BrandRepository : GenericRepository<Brand>
    {
        public BrandRepository(DataContext context) : base(context)
        {
        }
    }
}
