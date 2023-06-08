using App.Services.ProductServices;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
      [Route("api/[controller]")]
      [ApiController]
      public class BrandController : ControllerBase
      {
            private readonly IBrandService _brandService;
            public BrandController(IBrandService brandService)
            {
                  _brandService = brandService;
            }

            [HttpGet]
            public IActionResult GetAll()
            {
                  return Ok(_brandService.GetAll());
            }

            [HttpGet("{id:int}")]
            public IActionResult GetById(int id)
            {
                  try
                  {
                        var result = _brandService.GetById(id);
                        return Ok(result);
                  }
                  catch (System.Exception)
                  {
                        return NotFound();
                  }
            }
      }

}
