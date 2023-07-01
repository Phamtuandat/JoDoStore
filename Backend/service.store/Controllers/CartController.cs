using App.Dtos;
using App.Models.Identity;
using App.Services.ProductServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
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
                  var user = await _userManager.GetUserAsync(this.User);
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
                  var user = await _userManager.GetUserAsync(this.User);
                  if (user == null)
                  {
                        return Unauthorized();
                  }
                  await _cartService.RemoveItemAsync(id, user);
                  return Ok();
            }

            [HttpPost]
            public async Task<ActionResult> AddAsync([FromBody] CartItemReq req)
            {
                  var user = await _userManager.GetUserAsync(this.User);
                  if (user == null)
                  {
                        return Unauthorized();
                  }
                  await _cartService.AddItemAsync(req.Id, req.Quantity, user);
                  return Ok();
            }
      }
}
