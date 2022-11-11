using Backend.Data;
using Backend.Models.Products;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Backend.Repositories.Product
{
    public class ProductRepository : GenericRepository<Models.Products.ProductModel>
    {
        public ProductRepository(DataContext context) : base(context){}

        public override IQueryable<ProductModel> All()
        {
            return base.All().Include(x => x.Brand).Include(x => x.Categories).Include(x => x.Media);
        }
        public override IEnumerable<ProductModel> Find(Expression<Func<ProductModel, bool>> predicate)
        {
            return base.Find(predicate);
        }
        public override ProductModel Add(ProductModel entity)
        {
            return base.Add(entity);
        }
        public override ProductModel? Get(int id)
        {
            return base.Get(id);
        }
        public override ProductModel Delete(ProductModel entity)
        {
            return base.Delete(entity);
        }
        public override ProductModel Update(ProductModel entity)
        {
            return base.Update(entity);
        }
        
    }
}
