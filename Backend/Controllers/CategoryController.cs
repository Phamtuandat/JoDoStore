using App.Queries;
using App.Services.ProductServices;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
            public IActionResult GetAll()
            {
                  var result = _categoryService.GetAll();
                  return Ok(result);
            }
      }
}
