using App.Data;
using App.Models.ProductModel;

namespace App.Repositories
{
      public class ProductCategoryRepository : GenericRepository<ProductCategory>
      {
            public ProductCategoryRepository(DataContext context) : base(context)
            {
            }
      }
}
