using Backend.Data;
using Backend.Models.Products;
using System.Linq.Expressions;

namespace Backend.Repositories.Product
{
    public class MediaRepository : GenericRepository<Media>
    {
        public MediaRepository(DataContext context) : base(context) {}

        public override Media Delete(Media existedMedia)
        {
            return base.Delete(existedMedia);
        }
        public override Media? Get(int id)
        {
            return base.Get(id);
        }
        public override Media Add(Media entity)
        {
            return base.Add(entity);
        }
        public override Media Update(Media entity)
        {
            return base.Update(entity);
        }
        public override IQueryable<Media> All()
        {
            return base.All();
        }
        public override IEnumerable<Media> Find(Expression<Func<Media, bool>> predicate)
        {
            return base.Find(predicate);
        }
    }
}
