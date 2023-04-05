using gearshop_dotnetapp.Models.Identity;
using gearshop_dotnetapp.Resources;
using gearshop_dotnetapp.Services.ProductServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace gearshop_dotnetapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly UserManager<User> _userManager;

        public CartController(ICartService cartService, UserManager<User> userManager)
        {
            _cartService = cartService;
            _userManager = userManager;
        }


        [HttpGet]
        public async Task<ActionResult> Get()
        {

            var userContext = HttpContext.User;
            var user = await _userManager.GetUserAsync(userContext);
            if (user == null)
            {
                return Unauthorized();
            }
            var result = await _cartService.GetCart(user);
            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var userContext = HttpContext.User;
            var user = await _userManager.GetUserAsync(userContext);
            if (user == null)
            {
                return Unauthorized();
            }
            await _cartService.RemoveItemAsync(id, user);
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> AddAsync( [FromBody] CartItemReq req)
        {
            var userContext = HttpContext.User;
            var user = await _userManager.GetUserAsync(userContext);
            if (user == null)
            {
                return Unauthorized();
            }
            await _cartService.AddItemAsync(req.Id, req.Quantity, user);
            return Ok();
        }
    }
}
