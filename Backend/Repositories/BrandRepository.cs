using App.Data;
using App.Models.ProductModel;

namespace App.Repositories
{
      public class BrandRepository : GenericRepository<Brand>
      {
            public BrandRepository(DataContext context) : base(context)
            {
            }
      }
}
