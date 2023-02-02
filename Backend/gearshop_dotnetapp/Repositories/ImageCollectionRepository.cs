using gearshop_dotnetapp.Data;
using gearshop_dotnetapp.Models.Product;
using gearshop_dotnetapp.Repositories;

namespace gearshop_dotnetapp
{
    public class ImageCollectionRepository : GenericRepository<ImageCollections>
    {
        public ImageCollectionRepository(DataContext context) : base(context)
        {
        }
        public override IQueryable<ImageCollections> All()
        {
            return base.All();
        }
    }
}