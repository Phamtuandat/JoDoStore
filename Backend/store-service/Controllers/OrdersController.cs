using App.Enums;
using App.Exceptions;
using App.Models.Identity;
using App.Models.OrderModel;
using App.Services.OrderServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace App.Controllers
{
      [Route("api/[controller]")]
      [ApiController]
      public class OrdersController : ControllerBase
      {
            private readonly IOrderService _orderService;
            private readonly UserManager<User> _userManager;

            public OrdersController(IOrderService orderService, UserManager<User> userManager)
            {
                  _orderService = orderService;
                  _userManager = userManager;
            }

            [EnableQuery(PageSize = 5)]
            [HttpGet]
            [Authorize(Roles = "Admin")]
            public IQueryable GetAll()
            {
                  var result = _orderService.GetAllOrders();
                  var totalCount = result.Count();
                  HttpContext.Response.Headers.Add("X-Pagination-Total-Count", totalCount.ToString());
                  return result;
            }
            [HttpGet("user")]
            public async Task<ActionResult> GetUserOrders()
            {
                  var userContext = HttpContext.User;
                  var user = await _userManager.GetUserAsync(userContext);
                  if (user == null)
                  {
                        return Unauthorized();
                  }
                  var result = _orderService.GetOrdersByUser(user);
                  return Ok(result);
            }
            [HttpPost]
            public async Task<ActionResult> CreateAsync(Order saveOrderResource)
            {
                  var userContext = HttpContext.User;
                  var user = await _userManager.GetUserAsync(userContext);
                  if (user == null)
                  {
                        return Unauthorized();
                  }
                  var order = await _orderService.CreateOrderAsync(saveOrderResource, user);
                  return Ok(order);

            }
            [HttpPatch("{id:int}")]
            [Authorize(Roles = "Admin")]
            public async Task<ActionResult> UpdateStatusAsync([FromQuery] int id, string status)
            {
                  if (Enum.TryParse(status, out OrderStatus orderStatus))
                  {
                        // update the order status in the database
                        var order = await _orderService.UpdateStatusAsync(id, orderStatus);
                        return Ok(order);
                  }
                  else
                  {
                        return BadRequest("Invalid order status.");
                  }
            }
            [HttpDelete("{id:int}")]
            public async Task<ActionResult> DeleteAsync(int id)
            {
                  try
                  {
                        var userContext = HttpContext.User;
                        var user = await _userManager.GetUserAsync(userContext);
                        if (user == null)
                        {
                              return Unauthorized();
                        }
                        await _orderService.DeleteOrderAsync(id);
                        return Ok();

                  }
                  catch (BaseException ex)
                  {
                        return StatusCode((int)ex.StatusCode, ex.Message);
                  }

            }
      }
}
