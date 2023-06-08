using App.Data;
using App.Models.ProductModel;
using Microsoft.EntityFrameworkCore;

namespace App.Repositories
{
      public class ProductRepository : GenericRepository<Product>
      {
            public ProductRepository(DataContext context) : base(context)
            {
            }
            public override IQueryable<Product> All()
            {
                  return base.All();
            }

      }
}
