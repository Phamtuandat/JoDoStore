using gearshop_dotnetapp.Data;
using gearshop_dotnetapp.Models.ProductModel;
using System.Linq.Expressions;

namespace gearshop_dotnetapp.Repositories
{
    public class CategoryRepository : GenericRepository<Category>
    {
        public CategoryRepository(DataContext context) : base(context)
        {
        }
    }
}
