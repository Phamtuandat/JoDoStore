using gearshop_dotnetapp.Models.Identity;
using gearshop_dotnetapp.Models.Product;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace gearshop_dotnetapp.Data
{
    public class DataContext : IdentityDbContext<User>
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public DataContext(DbContextOptions<DataContext> options) : base(options)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connetionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
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

            builder.Entity<Product>()
                .HasOne(p => p.Brand)
                .WithMany(b => b.Products);
        }
    }
}
