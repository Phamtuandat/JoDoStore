using Backend.Extentions;
using gearshop_dotnetapp.Models.Product;
using gearshop_dotnetapp.Services.ProductServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace gearshop_dotnetapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThumbnailController : ControllerBase
    {
        private readonly IPhotoService _photoService;
        public ThumbnailController(IPhotoService photoService)
        {
            _photoService = photoService;
        }

        [HttpGet]
        [EnableQuery]
        public async Task<IActionResult> GetAll()
        {
            var list = await _photoService.GetAllThumbnails();
            return Ok(list);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] SavPhotoResource model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.GetErrorMessages());

            var result = await _photoService.CreateAsync(model);
            if(!result.Success) return BadRequest(result.Message);
            return Ok(result.Thumbnail);
        }

        [HttpGet("{id:int}")]
        public  IActionResult GetById( int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.GetErrorMessages());

            var result = _photoService.GetThumbnailById(id);
            if(!result.Success) return BadRequest(result.Message);
            return Ok(result.Thumbnail);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _photoService.DeleteAsync(id);
            if(!result.Success) return BadRequest(result.Message);
            return Ok();
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> UpdateAsync([FromForm] SavPhotoResource model, int id)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState.GetErrorMessages());
            var result = await _photoService.UpdateAsync(model, id);
            if (!result.Success) return BadRequest(result.Message);
            return Ok(result.Thumbnail);
        }
        
    }
}
