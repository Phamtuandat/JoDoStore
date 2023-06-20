using App.Data;

namespace App.Repositories
{
      public abstract class GenericRepository<T> : IRepository<T> where T : class
      {
            protected DataContext _context;
            public GenericRepository(DataContext context)
            {
                  _context = context;
            }

            public virtual T Add(T entity)
            {
                  return _context
                      .Add(entity)
                      .Entity;
            }

            public virtual IQueryable<T> All()
            {
                  return (from c in _context.Set<T>() select c)
                      .AsQueryable();
            }

            public virtual T Delete(T entity)
            {
                  return _context.Remove<T>(entity).Entity;
            }



            public virtual IEnumerable<T> Find(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
            {
                  return _context.Set<T>()
                      .AsQueryable()
                      .Where(predicate).ToList();
            }

            public virtual T? Get(int id)
            {
                  return _context.Set<T>().Find(id);
            }

            public async Task SaveChangesAsync()
            {
                  await _context.SaveChangesAsync();
            }

            public virtual T Update(T entity)
            {
                  return _context.Update(entity).Entity;
            }
      }
}
