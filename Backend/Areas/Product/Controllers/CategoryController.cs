using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.Models.ProductModel;
using App.Services.ProductServices;
using App.Utilities;
using Microsoft.AspNetCore.Authorization;
using App.Data;
using App.Enums;

namespace App.Areas.Products.Controllers
{
      [Authorize(Roles = RoleNames.Administrator)]
      [Route("/Category/[action]")]
      [Area("Product")]
      [ApiExplorerSettings(IgnoreApi = true)]
      public class CategoryController : Controller
      {
            private readonly ICategoryService _categoryService;
            [TempData]
            public string? Message { get; set; }

            public CategoryController(ICategoryService categoryService)
            {
                  _categoryService = categoryService;
            }


            // GET: Category
            public async Task<IActionResult> Index()
            {
                  var categories = await _categoryService.GetAll().ToListAsync();
                  var result = categories.Where(c => c.ParentCategory == null).ToList();
                  return View(result);
            }

            // GET: Category/Details/5
            public IActionResult Details(int? id)
            {

                  var category = _categoryService.GetAll().FirstOrDefault(c => c.Id == id);
                  if (category == null)
                  {
                        return NotFound();
                  }

                  return View(category);
            }

            // GET: Category/Create
            public IActionResult Create()
            {
                  try
                  {
                        var categoryList = _categoryService.GetAll().Where(c => c.ParentCategory == null).ToList();
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
                        ViewData["Genders"] = new SelectList(genderItems);
                        CreateSelectItem(categoryList, items, 0);
                        ViewData["ParentCategoryId"] = new SelectList(items, "Id", "Name");

                        return View();
                  }
                  catch (System.Exception)
                  {

                        throw;
                  }

            }

            // POST: Category/Create
            // To protect from overposting attacks, enable the specific properties you want to bind to.
            // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create([Bind("Name,Description,Slug,ParentCategoryId,Gender")] Category category)
            {
                  if (category.Slug == null) category.Slug = AppUtilities.GenerateSlug(category.Name);
                  if (_categoryService.GetAll().Where(c => c.Slug == category.Slug) != null)
                  {

                  }
                  if (ModelState.IsValid)
                  {
                        await _categoryService.SaveCategoryAsync(category);
                        return RedirectToAction(nameof(Index));
                  }
                  var categoryList = _categoryService.GetAll().Where(c => c.ParentCategory == null).ToList();
                  categoryList.Insert(0, new Category()
                  {
                        Name = "None",
                        Id = -1,
                  });
                  var items = new List<CategorySelecItem>();
                  CreateSelectItem(categoryList, items, 0);
                  ViewData["ParentCategoryId"] = new SelectList(items, "Id", "Name", category.ParentCategoryId);
                  return View(category);
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
            // GET: Category/Edit/5
            public IActionResult Edit(int id)
            {

                  var category = _categoryService.GetAll().FirstOrDefault(c => c.Id == id);
                  if (category == null)
                  {
                        return NotFound();
                  }
                  var categoryList = _categoryService.GetAll().Where(c => c.ParentCategory == null).ToList();
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
                  ViewData["Genders"] = new SelectList(genderItems);
                  CreateSelectItem(categoryList, items, 0);
                  if (category.ChildCategories?.Count > 0)
                  {
                        var categoryEditItem = items.FirstOrDefault(item => item.Id == category.Id);
                        items = items.Where(c => c.Id != id && c.level <= categoryEditItem.level).ToList();
                  }
                  ViewData["ParentCategoryId"] = new SelectList(items, "Id", "Name", category.ParentCategoryId);
                  return View(category);
            }

            // POST: Category/Edit/5
            // To protect from overposting attacks, enable the specific properties you want to bind to.
            // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Slug,ParentCategoryId,Gender")] Category category)
            {
                  var editCategory = _categoryService.GetAll().FirstOrDefault(c => c.Id == id);
                  if (id != category.Id || editCategory == null)
                  {
                        return NotFound();
                  }
                  var isValid = canUpdateCategory(editCategory.ChildCategories.ToList(), category);
                  if (!isValid)
                  {
                        Message = "Can't update with parent category is already a children of this category";
                  }

                  if (ModelState.IsValid && isValid)
                  {
                        try
                        {
                              if (category.ParentCategoryId == -1)
                              {
                                    category.ParentCategoryId = null;
                              }
                              await _categoryService.UpdateCategoryAsync(category);
                        }
                        catch (Exception)
                        {

                              throw;
                        }
                        return RedirectToAction(nameof(Index));
                  }
                  var categoryList = _categoryService.GetAll().Where(c => c.ParentCategory == null).ToList();
                  categoryList.Insert(0, new Category()
                  {
                        Name = "None",
                        Id = -1,

                  });
                  var items = new List<CategorySelecItem>();
                  CreateSelectItem(categoryList, items, 0);
                  ViewData["ParentCategoryId"] = new SelectList(items, "Id", "Name", category.ParentCategoryId);
                  return View(category);
            }

            // GET: Category/Delete/5
            public IActionResult Delete(int id)
            {
                  var category = _categoryService.GetById(id);
                  if (category == null)
                  {
                        return NotFound();
                  }

                  return View(category);
            }

            // POST: Category/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmed(int id)
            {

                  try
                  {
                        await _categoryService.DeleteAsync(id);
                        return RedirectToAction(nameof(Index));
                  }
                  catch (Exception ex)
                  {

                        throw new InvalidOperationException(ex.Message);
                  }

            }

            private bool canUpdateCategory(List<Category> categories, Category category)
            {
                  var parentCategoryId = category.ParentCategoryId;
                  if (categories.Count() == 0 || parentCategoryId == -1)
                  {
                        return true;
                  }
                  else if (categories.Count() > 0)
                  {
                        foreach (Category c in categories)
                        {
                              if (c.Id == parentCategoryId) return false;
                              if (c.ChildCategories == null) continue;
                              return canUpdateCategory(c.ChildCategories.ToList(), category);
                        }
                  }
                  return false;
            }
      }
}
