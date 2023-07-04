using App.Dtos;
using App.Services;
using App.Services.ProductServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace App.Controllers
{
      [Route("api/[controller]")]
      [ApiController]
      [Authorize]
      public class CartController : ControllerBase
      {
            private readonly ICartService _cartService;
            private readonly IDatabase _redis;
            private readonly IAuthService _authSevice;
            public CartController(ICartService cartService, IConnectionMultiplexer muxer, IAuthService authSevice)
            {
                  _cartService = cartService;
                  _redis = muxer.GetDatabase();
                  _authSevice = authSevice;
            }
            [HttpGet]
            public async Task<ActionResult> Get()
            {
                  var accessToken = await HttpContext.GetTokenAsync("access_token");
                  
                  var userinfo = await _authSevice.GetUserInfoAsync(accessToken);
                  var userId = userinfo.Sub;
                  var result = await _cartService.GetCart(userId);
                  return Ok(result);
            }
            [HttpDelete("{id:int}")]
            public async Task<ActionResult> DeleteAsync(int id)
            {

                  var accessToken = await HttpContext.GetTokenAsync("access_token");
                  var userinfo = await _authSevice.GetUserInfoAsync(accessToken);
                  var userId = userinfo.Sub;
                  await _cartService.RemoveItemAsync(id, userId);
                  return Ok();
            }

            [HttpPost]
            public async Task<ActionResult> AddAsync([FromBody] CartItemResource req)
            {
                  var accessToken = await HttpContext.GetTokenAsync("access_token");
                  var userinfo = await _authSevice.GetUserInfoAsync(accessToken);
                  var userId = userinfo.Sub;
                  await _cartService.AddItemAsync(req.ProductId, req.Quantity, userId);
                  return Ok();
            }
      }
}
