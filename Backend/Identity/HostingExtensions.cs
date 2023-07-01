using System.IdentityModel.Tokens.Jwt;
using Duende.IdentityServer;
using Identity.Data;
using Identity.Helpers;
using Identity.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
namespace Identity;

internal static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddRazorPages();

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
        builder.Services
            .AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
                options.EmitStaticAudienceClaim = true;
            })
            .AddInMemoryIdentityResources(Config.IdentityResources)
            .AddInMemoryApiScopes(Config.ApiScopes)
            .AddInMemoryClients(Config.Clients)
            .AddAspNetIdentity<ApplicationUser>();
        
        builder.Services.AddAuthentication(options => {
                options.DefaultScheme = "Cookies";
                options.DefaultChallengeScheme = "oidc";
        })
        .AddCookie("Cookies", options =>{
              // add an instance of the patched manager to the options:
                options.CookieManager = new ChunkingCookieManager();
                options.Cookie.SameSite = SameSiteMode.None;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        })
        .AddJwtBearer(options =>
        {
            options.SecurityTokenValidators.Clear();
            options.SecurityTokenValidators.Add(new JwtSecurityTokenHandler());
        })
        .AddGoogle(options =>
        {
            options.CorrelationCookie.SecurePolicy = CookieSecurePolicy.Always;
            options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
            options.ClientId = "285728072656-1vv64lpdqkti5gemi5fvgscvolb930sj.apps.googleusercontent.com";
            options.ClientSecret = "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";
            options.CallbackPath = "/signin-google";
        });
        builder.Services.AddCors(options =>
        {
            if(builder.Environment.IsDevelopment()){
                options.AddDefaultPolicy(policy =>
                {
                    policy.WithOrigins("http://localhost:3000")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
                });
            }else{
                    options.AddDefaultPolicy(policy =>
                    {
                        policy.WithOrigins("https://diydevblog.com")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                    });
                    
            }
        });
        
        builder.Services.Configure<CookiePolicyOptions>(options => {
            options.MinimumSameSitePolicy = SameSiteMode.Unspecified;
            options.Secure = CookieSecurePolicy.SameAsRequest;
            options.OnAppendCookie = cookieContext =>
                AuthenticationHelpers.CheckSameSite(cookieContext.Context, cookieContext.CookieOptions);
            options.OnDeleteCookie = cookieContext =>
                AuthenticationHelpers.CheckSameSite(cookieContext.Context, cookieContext.CookieOptions);
        });
        if (builder.Environment.IsProduction())
        {
            builder.WebHost.ConfigureKestrel(options =>
            {
                options.ListenAnyIP(5001);
            });
        }
        return builder.Build();
    }
    
    public static WebApplication ConfigurePipeline(this WebApplication app)
    { 
        app.UseSerilogRequestLogging();
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        app.UseCors();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthorization();
        
        app.MapRazorPages()
            .RequireAuthorization();
        using (var scope = app.Services.CreateScope())
        {
            
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<ApplicationDbContext>();
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
        }
        app.Use((context, next) =>
        {
            context.Request.Scheme = "https";
            return next();
        });
        app.UseIdentityServer();
        return app;
    }
    
}