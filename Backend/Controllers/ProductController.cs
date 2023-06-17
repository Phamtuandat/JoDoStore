using System.Collections.Specialized;
using System.Text.Json;
using System.Web;
using App.Models;
using App.Models.ProductModel;
using App.Services.ProductServices;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
      [Route("api/[controller]")]
      [ApiController]
      public class ProductController : ControllerBase
      {
            private readonly ILogger<ProductController> _logger;
            private readonly IProductService _productService;

            public ProductController(ILogger<ProductController> logger, IProductService productService)
            {
                  _logger = logger;
                  _productService = productService;
            }

            [HttpGet("/api/products")]
            public async Task<IActionResult> GetAll([FromQuery] ProductQs query)
            {
                  var result = await _productService.GetAllAsync(query);
                  var paginationMetadata = new PaginationMetadata()
                  {
                        CurrentPage = query.CurrentPage,
                        PageSize = query.PageSize,
                        TotalItems = result.Count(),
                  };
                  var stringJson = JsonSerializer.Serialize(paginationMetadata);
                  HttpContext.Response.Headers.Add("X-Pagination", stringJson);
                  return Ok(result);
            }
            [HttpGet]
            public async Task<IActionResult> GetAll()
            {
                  var result = _productService.GetAllAsync();
                  var totalCount = result.Count();
                  HttpContext.Response.Headers.Add("X-Pagination-Total-Count", totalCount.ToString());
                  return Ok(result.ToList());
            }


            [HttpGet("{id:int}")]
            public async Task<IActionResult> GetById(int id)
            {
                  try
                  {
                        var result = _productService.GetById(id);
                        return Ok(result);
                  }
                  catch (Exception ex)
                  {

                        return NotFound(ex.Message);
                  }
            }
      }
}
