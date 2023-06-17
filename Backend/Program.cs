using App;
using App.Data;
using App.Models.Identity;
using App.Repositories;
using App.Services;
using App.Services.EmailService;
using App.Services.OrderServices;
using App.Services.ProductServices;
using App.Settings;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging.Configuration;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
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
    .Expand()
    .SetMaxTop(100));
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
builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
{
      options.TokenLifespan = TimeSpan.FromDays(1);
});
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
}).AddEntityFrameworkStores<DataContext>()
.AddDefaultTokenProviders()
.AddTokenProvider<DataProtectorTokenProvider<User>>(TokenOptions.DefaultEmailProvider);
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
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddLogging(loggingBuilder =>
    {
          loggingBuilder.ClearProviders(); // Optional: Clear any default logging providers if needed

          // Configure your desired logging provider(s) here
          loggingBuilder.AddConsole(); // Example: Add the console logger
          loggingBuilder.AddDebug(); // Example: Add the debug logger
                                     // Add other logging providers as needed
    });
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IIconService, IConService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<ICartService, CartService>();

builder.Services.AddTransient<AppbarService>();
builder.Services.AddTransient<IActionContextAccessor, ActionContextAccessor>();
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services
        .AddAuthentication()
        .AddGoogle(option =>
        {
              var gconfig = builder.Configuration.GetSection("Authentication:Google");
              option.ClientId = gconfig["ClientId"];
              option.ClientSecret = gconfig["ClientSecrect"];
              option.CallbackPath = "/login-with-google";
        });

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

                                          "http://localhost:3000").AllowCredentials().AllowAnyMethod().AllowAnyHeader().AllowCredentials().WithExposedHeaders("X-Pagination");
                  }
                  policy.WithOrigins("https://phamtuandat.click",
                                    "https://phamtuandat.click").AllowCredentials().AllowAnyMethod().AllowAnyHeader().AllowCredentials();
            });
});

builder.Services.AddControllers();

var app = builder.Build();
// Configure the HTTP request pipeline.
app.UseRouting();

string path;
if (app.Environment.IsDevelopment())
{
      app.UseSwagger();
      app.UseSwaggerUI();
      app.UseHsts();

      app.UseHttpsRedirection();
      path = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
}
else
{
      path = "/app/Uploads";

}
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();

if (!Directory.Exists(path))
{
      Directory.CreateDirectory(path);
}

app.UseStaticFiles(new StaticFileOptions()
{
      FileProvider = new PhysicalFileProvider(
       path
    ),
      RequestPath = "/contents"
});

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
