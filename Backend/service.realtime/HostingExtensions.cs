using RealtimeApp.Hub;
using RealtimeApp.Models;
using Serilog;
using StackExchange.Redis;
using RealtimeApp.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using IdentityModel.Client;
using RealtimeApp.Services;
using Microsoft.IdentityModel.Tokens;

internal static class HostingExtensions
{

      public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
      {

            builder.Host.UseSerilog((context, services, configuration) => configuration
                  .ReadFrom.Configuration(context.Configuration)
                  .ReadFrom.Services(services)
                  .Enrich.FromLogContext());

            builder.Logging.ClearProviders();
            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddControllersWithViews();

            //config and inject mongodb strings
            builder.Services.Configure<RealtimeDbSettings>(builder.Configuration.GetSection(nameof(RealtimeDbSettings)));
            builder.Services.AddSingleton<IRealtimeDbSettings, RealtimeDbSettings>();
            builder.Services.AddScoped<IConnectRepository, ConnectRepository>();
            builder.Services.AddScoped<ITokenService, TokenService>();
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
            builder.Services.AddAccessTokenManagement(options =>
            {
                  options.Client.Clients.Add("identityserver", new ClientCredentialsTokenRequest
                  {
                        Address = "https://localhost:5001/connect/token",
                        ClientId = "client",
                        ClientSecret = "secret",
                        Scope = "api"
                  });
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

            builder.Services
            .AddAuthentication("Bearer")
            .AddJwtBearer("Bearer", options =>
            {
                  options.Authority = "https://localhost:5001";

                  options.TokenValidationParameters = new TokenValidationParameters
                  {
                        ValidateAudience = false
                  };
            });
                  builder.Services.AddAuthorization(options =>
            {
                  options.AddPolicy("ApiScope", policy =>
                  {
                        policy.RequireAuthenticatedUser();
                        policy.RequireClaim("scope2", "openid", "store-api");
                  });
            });

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

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddRazorPages();
            if (builder.Environment.IsProduction())
            {
                  builder.WebHost.ConfigureKestrel(options =>
                  {
                        options.ListenAnyIP(4431);
                  });
            }

            builder.Services.AddCors(options =>
            {
                  options.AddDefaultPolicy(
                  policy =>
                  {
                        if (builder.Environment.IsDevelopment())
                        {
                              policy.WithOrigins("http://localhost:3000","http://localhost:3000").AllowCredentials().AllowAnyMethod().AllowAnyHeader().AllowCredentials().WithExposedHeaders("X-Pagination");
                        }
                  });
            });

            return builder.Build();
      }
      public static WebApplication ConfigurePipeline(this WebApplication app)
      {


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                  app.UseSwagger();
                  app.UseSwaggerUI();
            }
            app.UseRouting();
            app.UseAuthentication();
            app.UseBff();
            app.UseAuthorization();

            app.MapBffManagementEndpoints();
            app.UseCors();
            // app.UseHttpsRedirection();
            app.MapRazorPages();

            app.MapControllers();

            app.MapHub<ChatHub>("/hubs/chat");
            return app;
      }
}
