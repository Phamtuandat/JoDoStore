using Backend.Extentions;
using gearshop_dotnetapp.Command.Product;
using gearshop_dotnetapp.Queries;
using gearshop_dotnetapp.Resources;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace gearshop_dotnetapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediatr;

        public CategoryController(IMediator mediatr)
        {
            _mediatr = mediatr;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateAsync([FromBody] SaveCategoryResource saveCategoryResource)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }
            var result  = await _mediatr.Send(new CreateCategoryCommand() { SaveCategoryResource = saveCategoryResource });
            if(result.Success) return StatusCode(201);
            return BadRequest(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediatr.Send(new GetAllCategoryQuery() { });
            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _mediatr.Send(new DeleteCategoryCommand() { Id = id });
            if(!result.Success) return BadRequest(result.Message);
            return Ok();
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> UpdateAsync(int id)
        {
            var result = await _mediatr.Send(new UpdateCategoryCommand() { Id = id });
            if(!result.Success) return BadRequest(result.Message.ToString());
            return Ok();
        }
    }
}
