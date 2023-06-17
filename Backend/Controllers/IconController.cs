using App.Services.ProductServices;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
      [Route("api/[controller]")]
      [ApiController]
      public class IconController : ControllerBase
      {
            private readonly IIconService _iconService;

            public IconController(IIconService iconService)
            {
                  _iconService = iconService;
            }

            [HttpGet]
            public IActionResult GetAll()
            {
                  var result = _iconService.GetAll();
                  return Ok(result);
            }
      }
}
