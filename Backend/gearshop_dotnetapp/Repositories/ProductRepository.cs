using gearshop_dotnetapp.Data;
using gearshop_dotnetapp.Models.Product;
using Microsoft.EntityFrameworkCore;

namespace gearshop_dotnetapp.Repositories
{
    public class ProductRepository : GenericRepository<Product>
    {
        public ProductRepository(DataContext context) : base(context)
        {
        }
        public override IQueryable<Product> All()
        {
            return base.All().Include(p => p.Thumbnails).Include(p => p.Category).Include(p => p.Brand).Include(p=>p.Tags);
        }
        public override Product? Get(int id)
        {
            return base.Get(id);
        }

    }
}
