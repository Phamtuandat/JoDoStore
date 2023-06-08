using App.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace App.Controllers
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
            [HttpGet]
            public async Task<IActionResult> GetAll()
            {
                  var result = await _mediatr.Send(new GetAllProductQuery() { });
                  var totalCount = result.Count();
                  HttpContext.Response.Headers.Add("X-Pagination-Total-Count", totalCount.ToString());
                  return Ok(result.ToList());
            }

            [HttpGet("{id:int}")]
            public async Task<IActionResult> GetById(int id)
            {
                  try
                  {
                        var result = await _mediatr.Send(new GetProductByIdQuery() { Id = id });
                        return Ok(result);
                  }
                  catch (Exception ex)
                  {

                        return NotFound(ex.Message);
                  }
            }
      }
}
