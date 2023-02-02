using Backend.Extentions;
using gearshop_dotnetapp.Services.ProductServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace gearshop_dotnetapp.Controllers
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
        public IActionResult GetById(int id) {
            var result  = _brandService.GetById(id);
            if(result.Success) return NotFound();
            return Ok(result.BrandResource);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(SaveBrandResource model)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState.GetErrorMessages());
            var result = await _brandService.CreateAsync(model);
            if(result.Success)
            {
                return StatusCode(200);
            }
            return BadRequest(result.Message);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _brandService.DeleteAsync(id);
            if(result.Success)
            {
                return StatusCode(200);
            }
            return BadRequest(result.Message);
        }
    }

}
