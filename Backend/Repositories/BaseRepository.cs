using Backend.Data;

namespace Backend.Repositories
{
    public abstract class BaseRepository
    {
        protected readonly DataContext _context;

        public BaseRepository(DataContext context)
        {
            _context = context;
        }
    }
}
