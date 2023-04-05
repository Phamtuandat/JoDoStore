using gearshop_dotnetapp.Data;
using gearshop_dotnetapp.Models.ProductModel;
using Microsoft.EntityFrameworkCore;

namespace gearshop_dotnetapp.Repositories
{
    public class CartRepository : GenericRepository<Cart>
    {
        public CartRepository(DataContext context) : base(context)
        {
        }

        public override IQueryable<Cart> All()
        {
            return base.All().Include(x => x.Items).ThenInclude(ci => ci.Product).ThenInclude(x => x.Thumbnails);
        }
    }
}
