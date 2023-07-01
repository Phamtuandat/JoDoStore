using App.Data;

namespace App.Repositories
{
      public abstract class GenericRepository
      {
            protected readonly DataContext _context;
            public GenericRepository(DataContext context) => _context = context;
      }
}
