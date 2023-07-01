using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Identity.Models;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Identity.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    private readonly IWebHostEnvironment _env;
    private readonly IConfiguration _config;  
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IWebHostEnvironment env, IConfiguration config)
        : base(options)
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
            connetionString = "Server=localhost;Port=1501;database=IdentityDb;username=postgres;password=admin;Pooling=true;";
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
    }
}
