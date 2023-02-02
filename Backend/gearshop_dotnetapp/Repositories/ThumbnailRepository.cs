using gearshop_dotnetapp.Data;
using gearshop_dotnetapp.Models.Product;
using Microsoft.EntityFrameworkCore;

namespace gearshop_dotnetapp.Repositories
{
    public class ThumbnailRepository : GenericRepository<Photo>
    {
        public ThumbnailRepository(DataContext context) : base(context)
        {
        }

        public override IQueryable<Photo> All()
        {
            return base.All().Include(t => t.ImageCollections);
        }
    }
}
