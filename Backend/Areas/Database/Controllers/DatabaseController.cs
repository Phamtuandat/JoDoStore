using App.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.Models.Identity;
using Microsoft.AspNetCore.Authorization;

[Area("Database")]
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
            // await SeedPostCategory(userAdmin);
            return RedirectToAction(nameof(Index));
      }
      // private async Task SeedPostCategory(User user)
      // {
      //       _context.Categories.RemoveRange(_context.Categories.Where(c => c.Description.Contains("[fakeData]")));
      //       _context.Posts.RemoveRange(_context.Posts.Where(p => p.Description.Contains("[fakeData]")));
      //       var fakerCategory = new Faker<Category>();
      //       int cm = 1;
      //       fakerCategory.RuleFor(c => c.Title, fk => $"CM{cm++}" + fk.Lorem.Sentence(1, 2).Trim('.'));
      //       fakerCategory.RuleFor(c => c.Description, fk => fk.Lorem.Sentence(5) + "[fakeData]");
      //       fakerCategory.RuleFor(c => c.Slug, fk => fk.Lorem.Slug());
      //       var cate = fakerCategory.Generate();
      //       var cate11 = fakerCategory.Generate();
      //       var cate12 = fakerCategory.Generate();
      //       var cate2 = fakerCategory.Generate();
      //       var cate21 = fakerCategory.Generate();
      //       var cate211 = fakerCategory.Generate();
      //       cate11.ParentCategory = cate;
      //       cate12.ParentCategory = cate;
      //       cate21.ParentCategory = cate2;
      //       cate211.ParentCategory = cate21;
      //       var categories = new List<Category>() { cate, cate11, cate12, cate2, cate21, cate211 };
      //       _context.Categories.AddRange(categories);

      //       var r = new Random();
      //       var fakerPost = new Faker<Post>();
      //       int bv = 1;
      //       fakerPost.RuleFor(c => c.Title, fk => $"Post {bv++} " + fk.Lorem.Sentence(1, 2).Trim('.'));
      //       fakerPost.RuleFor(c => c.Slug, fk => fk.Lorem.Slug());
      //       fakerPost.RuleFor(c => c.Description, fk => fk.Lorem.Sentence(5) + "[fakeData]");
      //       fakerPost.RuleFor(c => c.Content, fk => fk.Lorem.Paragraphs(7));
      //       fakerPost.RuleFor(c => c.CreatAt, fk => fk.Date.Between(DateTime.SpecifyKind(DateTime.Now.AddYears(-10), DateTimeKind.Utc), DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc)));
      //       fakerPost.RuleFor(c => c.Published, fk => true);
      //       fakerPost.RuleFor(c => c.AuthorId, fk => user.Id);
      //       List<Post> posts = new();
      //       List<PostCategory> postCategories = new();
      //       for (int i = 0; i < 40; i++)
      //       {
      //             var post = fakerPost.Generate();
      //             post.EditAt = post.CreatAt;
      //             posts.Add(post);
      //             postCategories.Add(new PostCategory()
      //             {
      //                   Category = categories[r.Next(5)],
      //                   Post = post
      //             });
      //       }
      //       _context.Posts.AddRange(posts);
      //       _context.PostCategories.AddRange(postCategories);

      //       // generate warehouse fake data

      //       await _context.SaveChangesAsync();
      // }
      // private async Task CreateWarehouseAsync()
      // {


      //       var r = new Random();
      //       char code = 'A';
      //       var fakerWarehouse = new Faker<WarehouseModel>();
      //       fakerWarehouse.RuleFor(c => c.Name, fk => $"WH {fk.Company}");
      //       fakerWarehouse.RuleFor(c => c.Address, fk => GenerateFakeAddress());
      //       fakerWarehouse.RuleFor(c => c.Code, fk => code++);
      //       fakerWarehouse.RuleFor(c => c.MaxX, 2);
      //       fakerWarehouse.RuleFor(c => c.MaxX, 2);
      //       fakerWarehouse.RuleFor(c => c.MaxX, 2);
      //       fakerWarehouse.RuleFor(c => c.HeightSize, 50);
      //       fakerWarehouse.RuleFor(c => c.ColumnSize, 10);
      //       fakerWarehouse.RuleFor(c => c.RowSize, 50);
      //       var warehouse1 = fakerWarehouse.Generate();
      //       var warehouse2 = fakerWarehouse.Generate();
      //       var warehouse3 = fakerWarehouse.Generate();
      //       var warehouse4 = fakerWarehouse.Generate();
      //       List<WarehouseModel> warehouses = _context.Warehouses.ToList();
      //       // _context.Warehouses.AddRange(warehouses);


      //       var fakeManufacturer = new Faker<Manufacturer>();
      //       fakeManufacturer.RuleFor(p => p.Name, fk => $"{fk.Name.FullName()} Group");
      //       fakeManufacturer.RuleFor(p => p.Address, fk => GenerateFakeAddress());
      //       fakeManufacturer.RuleFor(p => p.Phone, fk => fk.Phone.PhoneNumber());
      //       var manufacturers1 = fakeManufacturer.Generate();
      //       var manufacturers2 = fakeManufacturer.Generate();
      //       var manufacturers3 = fakeManufacturer.Generate();
      //       var manufacturers4 = fakeManufacturer.Generate();
      //       var manufacturers5 = fakeManufacturer.Generate();

      //       // List<Manufacturer> manufacturers = _context.Manufacturers.ToList();
      //       List<Manufacturer> manufacturers = new List<Manufacturer>() { manufacturers1, manufacturers2, manufacturers3, manufacturers4, manufacturers5 };
      //       _context.Manufacturers.AddRange(manufacturers);

      //       List<Coordinate> coordinates = _context.Coordinates.ToList();

      //       List<Location> locations = new List<Location>();
      //       var fakerProduct = new Faker<Product>();
      //       fakerProduct.RuleFor(p => p.Name, fk => fk.Commerce.Product());
      //       fakerProduct.RuleFor(p => p.Price, fk => fk.Random.Decimal(0, 1000));
      //       fakerProduct.RuleFor(p => p.ManufactureDate, fk => fk.Date.Between(DateTime.SpecifyKind(DateTime.Now.AddYears(-2), DateTimeKind.Utc), DateTime.SpecifyKind(DateTime.Now.AddMonths(-1), DateTimeKind.Utc)));
      //       fakerProduct.RuleFor(p => p.ExpirationDate, fk => fk.Date.Between(DateTime.SpecifyKind(DateTime.Now.AddYears(+5), DateTimeKind.Utc), DateTime.SpecifyKind(DateTime.Now.AddMonths(+3), DateTimeKind.Utc)));
      //       fakerProduct.RuleFor(p => p.Quantity, fk => fk.Random.Int(100));
      //       List<Product> products = new();
      //       for (var i = 0; i < 40; i++)
      //       {
      //             var product = fakerProduct.Generate();
      //             var startCoordinate = coordinates[r.Next(99)];
      //             var warehouse = warehouses[r.Next(3)];
      //             var location = GetLocation(startCoordinate, 1, 1, 2, warehouse.Code, coordinates);
      //             if (location == null) continue;
      //             location.Warehouse = warehouse;
      //             product.Manufacturer = manufacturers[r.Next(3)];
      //             product.Slug = AppUtilities.GenerateSlug(product.Name + "-" + i);
      //             product.Blocks = new List<Block>(){
      //                   new Block()
      //                   {
      //                         Product = product,
      //                         SizeX = 1,
      //                         SizeY = 1,
      //                         SizeZ = 2,
      //                         Location = location,
      //                   }
      //             };
      //             locations.Add(location);
      //             products.Add(product);
      //       }
      //       _context.Locations.AddRange(locations);
      //       _context.Products.AddRange(products);

      //       await _context.SaveChangesAsync();
      // }

      // private Location? GetLocation(Coordinate startCoordinate, int sizeX, int sizeY, int sizeZ, char warehouseCode, List<Coordinate> coordinates)
      // {
      //       List<Coordinate> coordinateOccupied = new List<Coordinate>();
      //       for (decimal z = startCoordinate.Z; z < sizeZ; z++)
      //       {
      //             for (decimal y = startCoordinate.Y; y < startCoordinate.Y + sizeY; y++)
      //             {
      //                   for (decimal x = startCoordinate.X; x < startCoordinate.X + sizeX; x++)
      //                   {
      //                         var coord = coordinates.Where(c => c.Slug == $"{x}-{y}-{z}-{warehouseCode}").FirstOrDefault();
      //                         if (coord != null)
      //                         {
      //                               if (coord.isOccupied == true) return null;
      //                               coord.isOccupied = true;
      //                               coordinateOccupied.Add(coord);
      //                         }
      //                   }
      //             }
      //       }
      //       _context.Coordinates.UpdateRange(coordinateOccupied);
      //       return new Location()
      //       {
      //             StartCoordinate = startCoordinate,
      //             Coordinates = coordinateOccupied,
      //       };
      // }
      // // private List<Coordinate> GenerateWarehouseCoordinates(List<WarehouseModel> warehouses)
      // // {
      // //       List<Coordinate> coordinates = new List<Coordinate>();
      // //       var r = new Random();
      // //       var unique = new Faker<string>();
      // //       foreach (var item in warehouses)
      // //       {
      // //             for (int z = 0; z < item.ColumnSize; z++)
      // //             {
      // //                   for (int y = 0; y < item.HeightSize; y++)
      // //                   {
      // //                         for (int x = 0; x < item.RowSize; x++)
      // //                         {
      // //                               Coordinate coordinate = new Coordinate()
      // //                               {
      // //                                     Z = z,
      // //                                     Y = y,
      // //                                     X = x,
      // //                                     Slug = $"{x}-{y}-{z}-{item.Code}"
      // //                               };
      // //                               coordinates.Add(coordinate);
      // //                         }
      // //                   }
      // //             }
      // //       }
      // //       return coordinates;
      // // }
      // private string GenerateFakeAddress()
      // {
      //       var faker = new Faker();
      //       string address = faker.Address.StreetAddress();
      //       string city = faker.Address.City();
      //       string state = faker.Address.State();
      //       string zipCode = faker.Address.ZipCode();
      //       string fakeAddress = $"{address}, {city}, {state}, {zipCode}";
      //       return fakeAddress;
      // }
}