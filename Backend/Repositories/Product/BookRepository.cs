using Backend.Data;
using Backend.Models.Products;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Backend.Repositories.Product
{
    public class BookRepository : GenericRepository<Book>
    {
        public BookRepository(DataContext context) : base(context){}

        public override IQueryable<Book> All()
        {
            return base.All().Include(x => x.Authors).Include(x => x.Categories);
        }
        public override IEnumerable<Book> Find(Expression<Func<Book, bool>> predicate)
        {
            return base.Find(predicate);
        }
        public override Book Add(Book entity)
        {
            return base.Add(entity);
        }
        public override Book Get(int id)
        {
            return base.Get(id);
        }
        public override Book Delete(Book entity)
        {
            return base.Delete(entity);
        }
        public override Book Update(Book entity)
        {
            return base.Update(entity);
        }
        
    }
}
