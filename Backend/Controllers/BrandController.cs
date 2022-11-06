using Backend.Models.Products;
using Backend.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Backend.Extentions;
using MediatR;
using Backend.Queries;
using Backend.Command;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BrandController(IMediator mediatr)
        {
            _mediator = mediatr;
        }

        [HttpGet] 
        public async Task<IActionResult> GetAll()
        {
            var resources = await _mediator.Send(new GetAllBrandQuery() );
            return Ok(resources);

        }
        
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetBrandByIdQuery() { Id = id});
            if(result.Success) return Ok(result.AuthorResource);
            return StatusCode(400, result.Message);

        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateAuthorAsync([FromBody] SaveAuthorResourceModel saveAuthorResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            var result = await _mediator.Send(new CreateAuthorCommand() { SaveAuthorResourceModel = saveAuthorResource });
            if (!result.Success) return StatusCode(400, result.Message);
            return Ok(result.AuthorResource);
        }
        [HttpPatch("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateAuthor([FromBody] SaveAuthorResourceModel resource, int id)
        {
            if (!ModelState.IsValid) 
                return BadRequest(ModelState.GetErrorMessages());
            var result = await _mediator.Send(new UpdateBrandCommand() { SaveBrandResourceModel = resource, Id = id });
            if (result.Success) return Ok(result.AuthorResource);
            return StatusCode(400, result.Message);
        }
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var result = await _mediator.Send(new DeleteBrandCommand() { Id = id});

            if (!result.Success) return StatusCode(400,result.Message);
            return Ok();
        }
    }
}
