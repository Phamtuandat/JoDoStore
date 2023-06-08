
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

            public ProductController(IProductService productService, ILogger<ProductController> logger, IMapper mapper, ICategoryService categoryService)
            {
                  _productService = productService;
                  _logger = logger;
                  _mapper = mapper;
                  _categoryService = categoryService;
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
                  var categories = await _categoryService.GetAll().ToListAsync();
                  ViewData["MultiSelectList"] = new MultiSelectList(categories, "Id", "Name");
                  return View();
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
                        var ProductCategories = new List<ProductCategory>();
                        // TODO: Add insert logic here
                        var productAt = _mapper.Map<CreateProducViewModel, Product>(product);
                        foreach (var id in product.CategoryIDs)
                        {
                              ProductCategories.Add(new ProductCategory()
                              {
                                    CategoryId = id,
                                    Product = productAt
                              });
                        }
                        productAt.ProductCategories = ProductCategories;
                        await _productService.CreateAsync(productAt);

                        return RedirectToAction(nameof(Index));
                  }
                  catch (Exception)
                  {
                        throw;
                  }
            }

            // GET: Product/Edit/5
            public async Task<ActionResult> Edit(int id)
            {
                  var product = _productService.GetAllAsync()
                        .Where(p => p.Id == id)
                        .FirstOrDefault();
                  if (product == null) return NotFound();
                  ViewData["images"] = product.ImagePaths;
                  var categories = await _categoryService.GetAll().ToListAsync();
                  var model = _mapper.Map<ProductDto, EditProductViewModel>(product);
                  model.CategoryIDs = product.ProductCategories.Select(pc => pc.CategoryId).ToArray();
                  ViewData["MultiSelectList"] = new MultiSelectList(categories, "Id", "Name", model.CategoryIDs);
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
                        var product = _mapper.Map<EditProductViewModel, Product>(model);
                        await _productService.UpdateAsync(product);
                        Message = "updated product successfully";
                        return RedirectToAction(nameof(Index));
                  }
                  catch (Exception ex)
                  {
                        Message = $"something went wrong {ex.Message}";
                        return View();
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