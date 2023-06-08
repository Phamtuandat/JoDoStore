using App.Data;
using App.Models.ProductModel;
using Microsoft.EntityFrameworkCore;


namespace App.Repositories
{
      public class CategoryRepository : GenericRepository<Category>
      {
            public CategoryRepository(DataContext context) : base(context)
            {
            }
            public override IQueryable<Category> All()
            {
                  var appDbContext = (from c in _context.Categories select c)
                                          .Include(c => c.ParentCategory)
                                          .Include(c => c.ChildCategories);

                  return appDbContext;
            }

      }
}
