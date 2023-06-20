using App.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.Models.Identity;
using App.Models.ProductModel;

[Area("Database")]
[ApiExplorerSettings(IgnoreApi = true)]
[Route("Database/[action]")]
public class DatabaseController : Controller
{
      private readonly UserManager<User> _userManager;
      private readonly RoleManager<IdentityRole> _roleManager;
      private readonly DataContext _context;
      private readonly ILogger<DatabaseController> _logger;
      public DatabaseController(DataContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, ILogger<DatabaseController> logger)
      {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
      }
      public ActionResult Index()
      {
            return View();
      }
      [HttpGet]
      public ActionResult DeleteDb()
      {
            return View();
      }
      [TempData]
      public string Message { get; set; } = string.Empty;
      [HttpPost]
      public async Task<ActionResult> DeleteDbAsync()
      {
            var success = await _context.Database.EnsureDeletedAsync();
            Message = success ? "Successfully delete database" : "Something went wrong";
            return RedirectToAction(nameof(Index));
      }
      [HttpPost]
      public async Task<ActionResult> Migration()
      {
            await _context.Database.MigrateAsync();
            Message = "Update database successfully!";
            return RedirectToAction(nameof(Index));
      }
      // public async Task<ActionResult> CreateWarehouseDatabase()
      // {
      //       await CreateWarehouseAsync();
      //       return RedirectToAction(nameof(Index));
      // }
      public async Task<ActionResult> SeedData()
      {
            var roleNames = typeof(RoleNames).GetFields().ToList();
            foreach (var item in roleNames)
            {
                  var roleName = item.GetRawConstantValue() as string;
                  if (roleName != null)
                  {
                        var role = await _roleManager.FindByNameAsync(roleName);
                        if (role == null)
                        {
                              role = new IdentityRole(roleName);
                              await _roleManager.CreateAsync(role);
                        }
                  }
            }
            var userAdmin = await _userManager.FindByEmailAsync("phamtuandat1a0@gmail.com");
            if (userAdmin == null)
            {
                  try
                  {
                        userAdmin = new User()
                        {
                              UserName = "admin",
                              Email = "phamtuandat1a0@gmail.com",
                              EmailConfirmed = true
                        };
                        await _userManager.CreateAsync(userAdmin, "phamdat11a1");
                        await _userManager.AddToRoleAsync(userAdmin, RoleNames.Administrator);
                  }
                  catch (Exception ex)
                  {
                        throw new DbUpdateException(ex.Message);
                  }
            }
            await SeedIcon();
            return RedirectToAction(nameof(Index));
      }
      private async Task SeedIcon()
      {
            List<Icon> iconList = new List<Icon>();
            var icons = new List<string>(){"Air Force 1", "Air Max", "Blazer", "Huarache", "Internationalist", "Mercurial", "Metcon", "Miler", "Nike Dunk"
                  ,"Pegasus",
                  "Phantom",
                  "Presto",
                  "Roshe",
                  "Structure",
                  "Tempo",
                  "Tiempo",
                  "Vomero",
                  "Wildhorse",
                  "ZoomRival",
                  "G.T. Series",
                  "Nike Invincible",};
            foreach (var item in icons)
            {
                  iconList.Add(new Icon()
                  {
                        Name = item,
                  });
            }
            _context.Icons.AddRange(iconList);
            await _context.SaveChangesAsync();
      }

}