using App.Services.ProductServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App.Controllers
{
      [Route("api/[controller]")]
      [ApiController]
      public class CategoryController : ControllerBase
      {
            private readonly ICategoryService _categoryService;

            public CategoryController(ICategoryService categoryService)
            {
                  _categoryService = categoryService;
            }

            [HttpGet]
            public async Task<IActionResult> GetAll()
            {
                  var categories = await _categoryService.GetAll().ToListAsync();
                  return Ok(categories);
            }
      }
}
