using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.Data;
using App.Models.ProductModel;
using App.Services.ProductServices;
using AutoMapper;
using App.Areas.Products.Models;

namespace App.Areas.Products.Controllers
{
      [Area("Product")]
      [Route("Icon/[Action]")]

      [ApiExplorerSettings(IgnoreApi = true)]
      public class IconController : Controller
      {
            private readonly IIconService _iconService;
            private readonly ICategoryService _categoryService;
            private readonly IMapper _mapper;
            private readonly ILogger<IconController> _log;

            public IconController(IIconService iconService, ICategoryService categoryService, ILogger<IconController> log, IMapper mapper)
            {
                  _iconService = iconService;
                  _categoryService = categoryService;
                  _log = log;
                  _mapper = mapper;
            }




            // GET: Icon
            public IActionResult Index()
            {
                  var icons = _iconService.GetAll().ToList();
                  return View(icons);
            }

            // GET: Icon/Details/5
            public IActionResult Details(int? id)
            {


                  var icon = _iconService
                              .GetAll()
                              .FirstOrDefault(m => m.Id == id);
                  if (icon == null)
                  {
                        return NotFound();
                  }

                  return View(icon);
            }

            // GET: Icon/Create
            public IActionResult Create()
            {
                  var categories = _categoryService.GetAll().ToList();
                  ViewData["SelectList"] = new MultiSelectList(categories, "Id", "Name");
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
            // POST: Icon/Create
            // To protect from overposting attacks, enable the specific properties you want to bind to.
            // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create([Bind("Id,Name,Description")] Icon icon)
            {
                  if (ModelState.IsValid)
                  {
                        await _iconService.CreateAsync(icon);
                        return RedirectToAction(nameof(Index));
                  }
                  return View(icon);
            }

            // GET: Icon/Edit/5
            public IActionResult Edit(int id)
            {
                  var icon = _iconService.GetAll().FirstOrDefault(x => x.Id == id);
                  if (icon == null)
                  {
                        return NotFound();
                  }
                  var model = _mapper.Map<Icon, CreateIconViewModel>(icon);
                  var categoryList = _categoryService.GetAll().Where(c => c.ParentCategory == null).ToList();
                  categoryList.Insert(0, new Category()
                  {
                        Name = "None",
                        Id = -1,
                  });
                  var items = new List<CategorySelecItem>();
                  CreateSelectItem(categoryList, items, 0);
                  return View(model);
            }

            // POST: Icon/Edit/5
            // To protect from overposting attacks, enable the specific properties you want to bind to.
            // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description, CategoryIDs")] CreateIconViewModel icon)
            {
                  if (id != icon.Id)
                  {
                        return NotFound();
                  }

                  if (ModelState.IsValid)
                  {
                        try
                        {
                              var ids = icon.CategoryIDs;

                              await _iconService.UpdateAsync(icon);
                        }
                        catch (DbUpdateConcurrencyException)
                        {
                              if (!IconExists(icon.Id))
                              {
                                    return NotFound();
                              }
                              else
                              {
                                    throw;
                              }
                        }
                        return RedirectToAction(nameof(Index));
                  }
                  return View(icon);
            }

            // GET: Icon/Delete/5
            public IActionResult Delete(int? id)
            {


                  var icon = _iconService.GetAll()
                      .FirstOrDefault(m => m.Id == id);
                  if (icon == null)
                  {
                        return NotFound();
                  }

                  return View(icon);
            }

            // POST: Icon/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmed(int id)
            {

                  await _iconService.DeleteAsync(id);
                  return RedirectToAction(nameof(Index));
            }

            private bool IconExists(int id)
            {
                  return (_iconService.GetAll().Any(e => e.Id == id));
            }
      }
}
