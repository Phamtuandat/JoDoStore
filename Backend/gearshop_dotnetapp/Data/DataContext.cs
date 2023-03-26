using Backend.Models.Identity;
using gearshop_dotnetapp.Models.Identity;
using gearshop_dotnetapp.Models.OrderModel;
using gearshop_dotnetapp.Models.ProductModel;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace gearshop_dotnetapp.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _config;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public DataContext(DbContextOptions<DataContext> options, IWebHostEnvironment env, IConfiguration config) : base(options)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            _env = env;
            _config = config;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            string? connetionString;
            if (_env.IsDevelopment())
            {
                connetionString = _config.GetConnectionString("GearShopDB");
            }
            else
            {
                connetionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
            }
                base.OnConfiguring(optionsBuilder);
                optionsBuilder
                    .UseNpgsql(connetionString);
        }


        public HashSet<Product> Products { get; set; }
        public HashSet<Category> Categories { get; set; }
        public HashSet<Brand> Brands { get; set; }
        public HashSet<Photo> Thumbnails { get; set; }
        public HashSet<ImageCollections> ImageCollections { get; set; }
        public HashSet<Tag> Tags { get; set; }
        public HashSet<Order> Orders { get; set; }
        public HashSet<OrderItem> OrderItems { get; set; }
        public HashSet<AddressBook> AddressBooks { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                var tableName = entityType.GetTableName();
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                if (tableName.StartsWith("AspNet"))
                {
                    entityType.SetTableName(tableName.Substring(6));
                }
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            }

            builder.Entity<Category>()
                .HasMany(c => c.Products)
                .WithOne(p => p.Category)
                .OnDelete(DeleteBehavior.SetNull);

           
            builder.Entity<ImageCollections>()
                .HasMany(c => c.Thumbnails)
                .WithOne(t => t.ImageCollections)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Photo>()
                .HasOne(p => p.Product)
                .WithMany(t => t.Thumbnails)
                .HasForeignKey(t => t.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
            
        }
    }
}
