using gearshop_dotnetapp.Data;
using gearshop_dotnetapp.Models.Product;

namespace gearshop_dotnetapp.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public UnitOfWork(DataContext context)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            _context = context;
        }

        private IRepository<Category> categoryRepository;
        public IRepository<Category> CategoryRepository
        {
            get
            {
                categoryRepository ??= new CategoryRepository(_context);
                return categoryRepository;
            }
        }

        private IRepository<Product> productRepository;
        public IRepository<Product> ProductRepository
        {
            get
            {
                productRepository ??= new ProductRepository(_context);
                return productRepository;
            }
        }

        private IRepository<Photo> thumbnailRepository;
        public IRepository<Photo> ThumbnailRepository
        {
            get
            {
                thumbnailRepository ??= new ThumbnailRepository(_context);
                return thumbnailRepository;
            }
        }

        private IRepository<Brand> brandRepository;
        public IRepository<Brand> BrandRepository
        {
            get
            {
                brandRepository ??= new BrandRepository(_context);
                return brandRepository;
            }
        }

        private IRepository<ImageCollections> imageCollectionsRepository;
        public IRepository<ImageCollections> ImageCollectionsRepository
        {
            get
            {
                imageCollectionsRepository ??= new ImageCollectionRepository(_context);
                return imageCollectionsRepository;
            }
        }

        private IRepository<Tag> tagRepository;
        public IRepository<Tag> TagRepository
        {
            get
            {
                tagRepository ??= new TagRepository(_context);
                return tagRepository;
            }
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
