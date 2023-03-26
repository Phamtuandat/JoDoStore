using Castle.Core.Resource;
using gearshop_dotnetapp;
using gearshop_dotnetapp.Data;
using gearshop_dotnetapp.Models.Identity;
using gearshop_dotnetapp.Models.OrderModel;
using gearshop_dotnetapp.Repositories;
using gearshop_dotnetapp.Services.OrderServices;
using gearshop_dotnetapp.Services.ProductServices;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging.Configuration;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using System.Reflection;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);


builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Services.AddControllers(options =>
{
    options.EnableEndpointRouting = false;
})
    .AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles)
        .AddOData(opt => opt.AddRouteComponents("odata", GetEdmModel())
    .Count()
    .Filter()
    .OrderBy()
    .Select()
    .Expand());
static IEdmModel GetEdmModel()
{
    var builder = new ODataConventionModelBuilder();
    return builder.GetEdmModel();
}
builder.Services.TryAddEnumerable(
    ServiceDescriptor.Singleton<ILoggerProvider, ColorConsoleLoggerProvider>());
LoggerProviderOptions.RegisterProviderOptions
    <ColorConsoleLoggerConfiguration, ColorConsoleLoggerProvider>(builder.Services);
// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckles
builder.Services.AddDbContext<DataContext>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.User.RequireUniqueEmail = true;  
    options.SignIn.RequireConfirmedEmail = false;
}).AddEntityFrameworkStores<DataContext>().AddDefaultTokenProviders(); 
builder.Services.ConfigureApplicationCookie(options =>
{
    // Cookie settings
    options.Cookie.HttpOnly = true;
    if (builder.Environment.IsProduction())
    {
        options.Cookie.SameSite = SameSiteMode.Lax;
    }
    else
    {
        options.Cookie.SameSite = SameSiteMode.None;
    }
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromDays(30); // set the expiration time to 7 days
    options.SlidingExpiration = true;
});

builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IBrandService, BrandService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IPhotoService, PhotoService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IAddredssService, AddressService>();
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

if (builder.Environment.IsProduction())
{
    builder.WebHost.ConfigureKestrel(options =>
    {
        options.ListenAnyIP(4432);
    });
}

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            if (builder.Environment.IsDevelopment())
            {
                policy.WithOrigins("http://localhost:3000",
                                    "http://localhost:3000").AllowCredentials().AllowAnyMethod().AllowAnyHeader().AllowCredentials().WithExposedHeaders("X-Pagination-Total-Count");
}
                policy.WithOrigins("https://phamtuandat.click",
                                    "https://phamtuandat.click").AllowCredentials().AllowAnyMethod().AllowAnyHeader().AllowCredentials();
        });
});


builder.Services.AddControllers();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();
app.MapControllers();
if (!builder.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;

        var context = services.GetRequiredService<DataContext>();
        if (context.Database.GetPendingMigrations().Any())
        {
            context.Database.Migrate();
        }
    }
}
app.Run();
