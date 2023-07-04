using App.Areas.Contact.Models;
using App.Models.Identity;
using App.Models.OrderModel;
using App.Models.ProductModel;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace App.Data
{
      public class DataContext : IdentityDbContext<User>
      {
            private readonly IWebHostEnvironment _env;
            private readonly IConfiguration _config;
            public DataContext(IWebHostEnvironment env, IConfiguration config) : base()
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
                        connetionString = "Server=localhost;Port=1501;database=JodoDb;username=postgres;password=admin;Pooling=true;";
                  }
                  else
                  {
                        connetionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
                  }
                  base.OnConfiguring(optionsBuilder);
                  optionsBuilder
                        .ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.MultipleCollectionIncludeWarning))
                        .UseNpgsql(connetionString);
                  optionsBuilder.EnableSensitiveDataLogging();
            }
            protected override void OnModelCreating(ModelBuilder builder)
            {

                  base.OnModelCreating(builder);
                  foreach (var entityType in builder.Model.GetEntityTypes())
                  {
                        var tableName = entityType.GetTableName();
                        if (tableName.StartsWith("AspNet"))
                        {
                              entityType.SetTableName(tableName.Substring(6));
                        }
                  }
                  builder.Entity<Product>(entity =>
                  {
                        entity.HasIndex(c => c.Slug).IsUnique();
                  });

                  builder.Entity<Category>(entity =>
                  {
                        entity.HasIndex(c => c.Slug);
                  });
                  builder.Entity<ProductCategory>(entity =>
                  {
                        entity.HasKey(c => new { c.CategoryId, c.ProductId });
                  });
            }

            public DbSet<ProductCategory> ProductCategory { get; set; }
            public DbSet<Cart> Carts { get; set; }
            public DbSet<CartItem> CartItems { get; set; }
            public DbSet<Product> Products { get; set; }
            public DbSet<Category> Categories { get; set; }
            public DbSet<Order> Orders { get; set; }
            public DbSet<OrderItem> OrderItems { get; set; }
            public DbSet<Address> Address { get; set; }
            public DbSet<Contact> Contacts { get; set; }
            public DbSet<Icon> Icons { get; internal set; }
      }
}
