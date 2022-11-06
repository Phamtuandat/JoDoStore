using Backend.Data;
using Backend.Models.Products;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace Backend.Repositories.Product
{
    public class BrandRepository : GenericRepository<BrandModel>
    {
        public BrandRepository(DataContext context) : base(context)
        {
        }

        public override BrandModel Delete(BrandModel existtingAuthor)
        {
            return base.Delete(existtingAuthor);
        }
        public override BrandModel? Get(int id)
        {
            return base.Get(id);
        }
        public override BrandModel Add(BrandModel entity)
        {
            return base.Add(entity);
        }
        public override BrandModel Update(BrandModel entity)
        {
            return base.Update(entity);
        }
        public override IQueryable<BrandModel> All()
        {
            return base.All();
        }
        public override IEnumerable<BrandModel> Find(Expression<Func<BrandModel, bool>> predicate)
        {
            return base.Find(predicate);
        }
        
    }
}
