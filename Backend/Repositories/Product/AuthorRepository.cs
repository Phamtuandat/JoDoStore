using Backend.Data;
using Backend.Models.Products;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace Backend.Repositories.Product
{
    public class AuthorRepository : GenericRepository<Author>
    {
        public AuthorRepository(DataContext context) : base(context)
        {
        }

        public override Author Delete(Author existtingAuthor)
        {
            return base.Delete(existtingAuthor);
        }
        public override Author Get(int id)
        {
            return base.Get(id);
        }
        public override Author Add(Author entity)
        {
            return base.Add(entity);
        }
        public override Author Update(Author entity)
        {
            return base.Update(entity);
        }
        public override IQueryable<Author> All()
        {
            return base.All();
        }
        public override IEnumerable<Author> Find(Expression<Func<Author, bool>> predicate)
        {
            return base.Find(predicate);
        }
        
    }
}
