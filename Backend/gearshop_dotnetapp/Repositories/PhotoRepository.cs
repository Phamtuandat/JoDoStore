using gearshop_dotnetapp.Data;
using gearshop_dotnetapp.Models.ProductModel;
using Microsoft.EntityFrameworkCore;

namespace gearshop_dotnetapp.Repositories
{
    public class PhotoRepository : GenericRepository<Photo>
    {
        public PhotoRepository(DataContext context) : base(context)
        {
        }

        public override IQueryable<Photo> All()
        {
            return base.All().Include(t => t.ImageCollections);
        }
    }
}
