using App;
using App.Data;
using App.Repositories;
using App.Services;
using App.Services.EmailService;
using App.Services.OrderServices;
using App.Services.ProductServices;
using App.Settings;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
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
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IIconService, IConService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddTransient<AppbarService>();
builder.Services.AddTransient<IActionContextAccessor, ActionContextAccessor>();
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddAuthentication("Bearer")
            .AddJwtBearer("Bearer", options =>
            {
                  options.Authority = "https://localhost:5001";
                  options.TokenValidationParameters = new TokenValidationParameters
                  {
                        ValidateAudience = false
                  };
            })
            .AddGoogle(option =>
            {
                  var gconfig = builder.Configuration.GetSection("Authentication:Google");
                  option.ClientId = gconfig["ClientId"];
                  option.ClientSecret = gconfig["ClientSecrect"];
                  option.CallbackPath = "/login-with-google";
            });
builder.Services.AddAuthorization(options =>
{
      options.AddPolicy("ApiScope", policy =>
      {
            policy.RequireAuthenticatedUser();
            policy.RequireClaim( "scope2", "openid", "store-api");
      });
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
      FileProvider = new PhysicalFileProvider(path),
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
