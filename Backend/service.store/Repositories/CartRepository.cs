using App.Data;
using App.Models.ProductModel;
using Microsoft.EntityFrameworkCore;

namespace App.Repositories
{
      public class CartRepository : GenericRepository<Cart>
      {
            public CartRepository(DataContext context) : base(context)
            {
            }

            public override IQueryable<Cart> All()
            {
                  return base.All();
            }
      }
}
