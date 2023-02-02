using gearshop_dotnetapp.Data;
using gearshop_dotnetapp.Models.Product;

namespace gearshop_dotnetapp.Repositories
{
    public class TagRepository : GenericRepository<Tag>
    {
        public TagRepository(DataContext context) : base(context)
        {
        }
    }
}
