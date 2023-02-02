using gearshop_dotnetapp.Data;

namespace gearshop_dotnetapp.Repositories
{
    public abstract class GenericRepository
    {
        protected readonly DataContext _context;
        public GenericRepository(DataContext context) => _context = context;
    }
}
