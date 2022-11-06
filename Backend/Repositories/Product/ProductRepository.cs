using Backend.Data;
using Backend.Models.Products;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Backend.Repositories.Product
{
    public class ProductRepository : GenericRepository<Models.Products.ProductModel>
    {
        public ProductRepository(DataContext context) : base(context){}

        public override IQueryable<Models.Products.ProductModel> All()
        {
            return base.All().Include(x => x.Brands).Include(x => x.Categories);
        }
        public override IEnumerable<Models.Products.ProductModel> Find(Expression<Func<Models.Products.ProductModel, bool>> predicate)
        {
            return base.Find(predicate);
        }
        public override Models.Products.ProductModel Add(Models.Products.ProductModel entity)
        {
            return base.Add(entity);
        }
        public override Models.Products.ProductModel? Get(int id)
        {
            return base.Get(id);
        }
        public override Models.Products.ProductModel Delete(Models.Products.ProductModel entity)
        {
            return base.Delete(entity);
        }
        public override Models.Products.ProductModel Update(Models.Products.ProductModel entity)
        {
            return base.Update(entity);
        }
        
    }
}
