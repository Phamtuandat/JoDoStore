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
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Serilog;
using StackExchange.Redis;
using System.Reflection;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);

//Config the application logger provider
builder.Host.UseSerilog((context, services, configuration) => configuration
      .ReadFrom.Configuration(context.Configuration)
      .ReadFrom.Services(services)
      .Enrich.FromLogContext());
builder.Logging.ClearProviders();


builder.Services.AddControllersWithViews();
builder.Services.AddControllers(options =>
{
      options.EnableEndpointRouting = false;
})
      .AddJsonOptions(x =>
                  x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme);
builder.Services.AddDbContext<DataContext>();
builder.Services.AddSingleton<IConnectionMultiplexer>(x =>
{
      if (builder.Environment.IsDevelopment())
      {
            var configuration = ConfigurationOptions.Parse(builder.Configuration["Redis:ConnectionString"]);
            return ConnectionMultiplexer.Connect(configuration);
      }
      else
      {
            var configuration = ConfigurationOptions
                                        .Parse(Environment.GetEnvironmentVariable("REDIS_CONNECTION_STRING"))
                                        .Password = Environment.GetEnvironmentVariable("REDIS_PASSWORD");
            return ConnectionMultiplexer.Connect(configuration);
      }
});
builder.Services.AddStackExchangeRedisCache(options =>
{
      if (builder.Environment.IsDevelopment())
      {
            options.Configuration = builder.Configuration["Redis:ConnectionString"];
            options.InstanceName = "JodoRedis";
      }
      else
      {
            options.Configuration = Environment.GetEnvironmentVariable("REDIS_CONNECTION_STRING");
            options.InstanceName = "JodoRedis";
      }
});
builder.Services.AddSignalR();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckles
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
      options.ExpireTimeSpan = TimeSpan.FromDays(30);
      options.SlidingExpiration = true;
      options.Cookie.Name = "application.Identity";
      if (builder.Environment.IsProduction())
      {
            options.Cookie.Domain = ".diydevblog.com";
      }
});
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
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
                        policy.WithOrigins("http://localhost:3000").AllowCredentials().AllowAnyMethod().AllowAnyHeader().AllowCredentials().WithExposedHeaders("X-Pagination");
                  }

            });
});


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
