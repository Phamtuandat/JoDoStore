using Backend.Extentions;
using gearshop_dotnetapp.Command.Product;
using gearshop_dotnetapp.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace gearshop_dotnetapp.Controllers
{
    [Route("api/[controller]")]
    [EnableQuery]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediatr;

        public ProductController(IMediator mediatr)
        {
            _mediatr = mediatr;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] SaveProductResource model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }
            var result = await _mediatr.Send(new CreateProductCommand() { SaveProductResource = model });
            if(!result.Success) return BadRequest(result.Message);
            return StatusCode(201);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _mediatr.Send(new DeleteProductCommand() { Id = id });
            if (!result.Success) return BadRequest(result.Message);
            return StatusCode(202);
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> UpdateAsync( SaveProductResource model, int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.GetErrorMessages());
            var result = await _mediatr.Send(new UpdateProducCommand() { SaveProductResource = model, Id = id });
            if (result.Success) return Ok(result.ProductResource);
            return BadRequest(result.Message);
        }

        [EnableQuery(PageSize = 9)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediatr.Send(new GetAllProductQuery() { });
            var totalCount = result.Count();
            HttpContext.Response.Headers.Add("X-Pagination-Total-Count", totalCount.ToString());
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediatr.Send(new GetProductByIdQuery() { Id = id});
            if (!result.Success) return BadRequest(result.Message);
            return Ok(result.ProductResource);
        }
    }
}
