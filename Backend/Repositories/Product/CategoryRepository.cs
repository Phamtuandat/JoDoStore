using Backend.Data;
using Backend.Models.Products;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Backend.Repositories.Product
{
    public class CategoryRepository : GenericRepository<Category>
    {
        public CategoryRepository(DataContext context) : base(context) { }

        public override IQueryable<Category> All()
        {
            return base.All();
        }
        public override Category Add(Category entity)
        {
            return base.Add(entity);
        }
        public override Category Delete(Category entity)
        {
            return base.Delete(entity);
        }
        public override Category Get(int id)
        {
            return base.Get(id);
        }
        public override Category Update(Category entity)
        {
            return base.Update(entity);
        }
        public override IEnumerable<Category> Find(Expression<Func<Category, bool>> predicate)
        {
            return base.Find(predicate);
        }
    }
}
