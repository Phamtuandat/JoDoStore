using Backend.Command;
using Backend.Queries;
using Backend.Resources;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Backend.Extentions;
using Backend.Models.Identity;
using Newtonsoft.Json;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableQuery]
    [Authorize(Roles = "Admin")]
    public class ProductController : ODataController
    {
        private readonly IMediator _mediator;
        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _mediator.Send(new GetAllProductQuery()));

        }
        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetProductByIdQuery() { Id = id });
            if (result.Success) return Ok(result.ProductResource);
            return StatusCode(404, result.Message);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] SaveProductResource model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.GetErrorMessages());
            var result = await _mediator.Send(new CreateProductCommand() { SaveBookResource = model });
            if (!result.Success)
            {
                return StatusCode(400, result.Message);
            };

            return Ok(result.ProductResource);
        }
        [HttpPatch("{id:int}")]

        public async Task<IActionResult> UpdateAsync([FromForm] SaveProductResource model, int id)
        {
            var result = await _mediator.Send(new UpdateProductCommand() { SaveBookResource = model, Id = id });
            if (result.Success) return Ok(result.ProductResource);
            return StatusCode(400, result.Message);
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _mediator.Send(new DeleteCategoryCommand() { Id = id });
            if (result.Success) return Ok();
            return StatusCode(400, result.Message);
        }

    }
}
    