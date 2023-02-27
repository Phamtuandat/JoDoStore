using gearshop_dotnetapp.Data;
using gearshop_dotnetapp.Models.ProductModel;

namespace gearshop_dotnetapp.Repositories
{
    public class TagRepository : GenericRepository<Tag>
    {
        public TagRepository(DataContext context) : base(context)
        {
        }
    }
}
