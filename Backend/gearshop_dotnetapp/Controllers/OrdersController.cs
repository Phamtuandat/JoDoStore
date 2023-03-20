using gearshop_dotnetapp.Enums;
using gearshop_dotnetapp.Exceptions;
using gearshop_dotnetapp.Models.Identity;
using gearshop_dotnetapp.Resources;
using gearshop_dotnetapp.Services.OrderServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace gearshop_dotnetapp.Controllers
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

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult GetAll() {
            var result = _orderService.GetAllOrders();
            return Ok(result);
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
        public async Task<ActionResult> CreateAsync(SaveOrderResource saveOrderResource) {
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
