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
                        .ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.MultipleCollectionIncludeWarning))
                        // other configuration options
                        .UseNpgsql(connetionString);
                  optionsBuilder.EnableSensitiveDataLogging();
            }

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
