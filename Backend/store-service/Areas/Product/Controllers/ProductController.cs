
using AutoMapper;
using App.Areas.Products.Models;
using App.Dtos;
using App.Models.ProductModel;
using App.Services.ProductServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.Data;
using Microsoft.AspNetCore.Authorization;

namespace App.Areas.Products.Controllers
{
      [Authorize(Roles = RoleNames.Administrator)]
      [Route("/Product/[action]")]
      [Area("Product")]
      [ApiExplorerSettings(IgnoreApi = true)]
      public class ProductController : Controller
      {
            private readonly IProductService _productService;
            private readonly ILogger<ProductController> _logger;
            private readonly IMapper _mapper;
            [TempData]
            public string? Message { get; set; }
            private readonly ICategoryService _categoryService;
            private readonly IIconService _iconService;

            public ProductController(IProductService productService, ILogger<ProductController> logger, IMapper mapper, ICategoryService categoryService, IIconService iconService)
            {
                  _productService = productService;
                  _logger = logger;
                  _mapper = mapper;
                  _categoryService = categoryService;
                  _iconService = iconService;
            }


            // GET: Product
            public ActionResult Index()
            {
                  var products = _productService.GetAllAsync();
                  return View(products);
            }

            // GET: Product/Details/5
            public ActionResult Details(int id)
            {
                  return View();
            }

            // GET: Product/Create
            public async Task<ActionResult> Create()
            {
                  var icons = _iconService.GetAll().ToList();
                  var categoryList = await _categoryService.GetAll().Where(c => c.ParentCategory == null).ToListAsync();
                  categoryList.Insert(0, new Category()
                  {
                        Name = "None",
                        Id = -1,
                  });
                  var items = new List<CategorySelecItem>();
                  var genders = typeof(Gender).GetFields().ToList();
                  var genderItems = new List<string>();
                  foreach (var gender in genders)
                  {
                        var item = gender.GetRawConstantValue() as string;
                        if (item != null)
                        {
                              genderItems.Add(item);
                        }
                  }
                  var technologies = typeof(Technologies).GetFields().ToList();
                  var techList = new List<string>();
                  foreach (var item in technologies)
                  {
                        var tech = item.GetRawConstantValue() as string;
                        if (tech != null)
                        {
                              techList.Add(tech);
                        }
                  }
                  ViewData["TechnologiesList"] = new SelectList(techList);
                  ViewData["Genders"] = new SelectList(genderItems);
                  CreateSelectItem(categoryList, items, 0);
                  ViewData["SelectList"] = new MultiSelectList(items, "Id", "Name");
                  ViewData["IconList"] = new SelectList(icons, "Id", "Name");
                  return View();
            }
            private void CreateSelectItem(List<Category> source, List<CategorySelecItem> des, int level)
            {
                  foreach (Category category in source)
                  {
                        var prefix = string.Concat(Enumerable.Repeat("---", level));
                        des.Add(new CategorySelecItem()
                        {
                              Id = category.Id,
                              Name = prefix + " " + category.Name,
                              level = level,

                        });
                        if (category.ChildCategories?.Count > 0)
                        {
                              CreateSelectItem(category.ChildCategories.ToList(), des, level + 1);
                        }
                  }
            }
            // POST: Product/Create
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<ActionResult> Create(CreateProducViewModel product)
            {
                  if (!ModelState.IsValid)
                  {
                        Message = "Product created is invalid.";
                        return View();
                  }
                  try
                  {
                        var productAt = _mapper.Map<CreateProducViewModel, Product>(product);

                        var productCategories = new List<ProductCategory>();
                        var cateIds = product.CategoryIDs;
                        foreach (var cateId in cateIds)
                        {
                              var productCategory = new ProductCategory()
                              {
                                    Product = productAt,
                                    CategoryId = cateId,
                              };
                              productCategories.Add(productCategory);
                        }
                        productAt.ProductCategories = productCategories;
                        await _productService.CreateAsync(productAt);
                        return RedirectToAction(nameof(Index));
                  }
                  catch (Exception ex)
                  {
                        Message = ex.Message;
                        return RedirectToAction(nameof(Index));
                  }
            }

            // GET: Product/Edit/5
            public async Task<ActionResult> Edit(int id)
            {
                  var icons = _iconService.GetAll().ToList();
                  var product = _productService.GetAllAsync()
                        .Where(p => p.Id == id)
                        .FirstOrDefault();
                  if (product == null) return NotFound();
                  ViewData["images"] = product.ImagePaths;
                  var technologies = typeof(Technologies).GetFields().ToList();
                  var techList = new List<string>();
                  foreach (var item in technologies)
                  {
                        var tech = item.GetRawConstantValue() as string;
                        if (tech != null)
                        {
                              techList.Add(tech);
                        }
                  }
                  ViewData["TechnologiesList"] = new SelectList(techList);
                  var categories = await _categoryService.GetAll().Where(c => c.ParentCategory == null).ToListAsync();
                  categories.Add(new Category()
                  {
                        Id = -1,
                        Name = "None"
                  });
                  var model = _mapper.Map<ProductDto, EditProductViewModel>(product);
                  var selected = product.productCategories.Select(c => c.CategoryId).ToArray();
                  var items = new List<CategorySelecItem>();
                  CreateSelectItem(categories, items, 0);
                  ViewData["SelectList"] = new MultiSelectList(items, "Id", "Name", selected);
                  ViewData["IconList"] = new SelectList(icons, "Id", "Name");
                  return View(model);
            }

            // POST: Product/Edit/5
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<ActionResult> Edit(int id, EditProductViewModel model)
            {
                  if (!ModelState.IsValid) return View(model);
                  try
                  {
                        // TODO: Add update logic here
                        await _productService.UpdateAsync(model);
                        Message = "updated product successfully";
                        return RedirectToAction(nameof(Index));
                  }
                  catch (Exception ex)
                  {
                        Message = $"something went wrong {ex.Message}";
                        return RedirectToAction(nameof(Index));
                  }
            }

            // GET: Product/Delete/5
            public ActionResult Delete(int id)
            {
                  return View();
            }

            // POST: Product/Delete/5
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<ActionResult> Delete(int id, IFormCollection collection)
            {
                  try
                  {
                        await _productService.DeleteAsync(id);
                        return RedirectToAction(nameof(Index));
                  }
                  catch
                  {
                        return View();
                  }
            }
      }
}