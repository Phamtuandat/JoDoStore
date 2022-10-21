using Backend.Extentions;
using Backend.Queries;
using Backend.Resources;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]

    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoriesController(IMediator mediatr)
        {
            _mediator = mediatr;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _mediator.Send(new GetAllCategoriesQuery()));
        }
        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _mediator.Send(new GetCategoryByIdQuery() { Id = id }));
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategory(SaveCategoryResource resource)
        {
            if (!ModelState.IsValid)
                return StatusCode(400, ModelState.GetErrorMessages());
            var result = await _mediator.Send(new CreateCategoryCommand() { SaveCategoryResource = resource });
            if (result.Success) return Ok(result.CategoryResource);
            return StatusCode(400, result.Message);
        }
        [HttpPatch("{id:int}")]
        public async Task<IActionResult> UpdateCategory(SaveCategoryResource resource, int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            var result = await _mediator.Send(new UpdateCategoryCommand() { SaveCategoryResource = resource });
            if (!result.Success)
                return StatusCode(400, result.Message);
            return Ok(result.CategoryResource);
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCategoryAsync(int id)
        {
            var result = await _mediator.Send(new DeleteCategoryCommand() { Id = id });
            if (!result.Success) return StatusCode(400, result.Message);
            return Ok();
        }
    }
}
