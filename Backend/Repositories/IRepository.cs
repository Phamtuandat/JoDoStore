using System.Linq.Expressions;

namespace App.Repositories
{
      public interface IRepository<T>
      {
            T Add(T entity);
            T Update(T entity);
            T? Get(int id);
            T Delete(T entity);
            IQueryable<T> All();
            IEnumerable<T>? Find(Expression<Func<T, bool>> predicate);
            Task SaveChangesAsync();
      }
}
