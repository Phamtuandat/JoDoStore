
using Backend.Data;
using Backend.Models.Products;
using Backend.Repositories.Product;

namespace Backend.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        public UnitOfWork(DataContext context)
        {
            _context = context;
        }

        private IRepository<BrandModel> authorRepository;
        public IRepository<BrandModel> BrandRepository
        {
            get
            {
                if(authorRepository == null)
                {
                    authorRepository = new BrandRepository(_context);
                }
                return authorRepository;
            }
        }
        private IRepository<Category> categoryRepository;
        public IRepository<Category> CategoryRepository 
        {
            get
            {
                if(categoryRepository == null)
                {
                    categoryRepository = new CategoryRepository(_context);
                }
                return categoryRepository;
            }
        }
        private IRepository<ProductModel> bookRepository;
        public IRepository<ProductModel> ProductRepository
        {
            get
            {
                if(bookRepository == null)
                {
                    bookRepository = new ProductRepository(_context);
                }
                return bookRepository;
            }
        }
        private IRepository<Media> mediaRepository;
        public IRepository<Media> MediaRepository
        {
            get
            {
                if (mediaRepository == null)
                {
                    mediaRepository = new MediaRepository(_context);
                }
                return mediaRepository;
            }
        }
        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
