using System.Security.Claims;
using IdentityModel;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealtimeApp.Services;

namespace RealtimeApp.Controllers;
[Route("identity")]
[Authorize]
public class IdentityController : ControllerBase
{
      private readonly ILogger<IdentityController> _logger;
      private readonly ITokenService _tokenService;

      public IdentityController(ITokenService tokenService, ILogger<IdentityController> logger)
      {
                  _logger = logger;
                  _tokenService = tokenService;
      }

      [HttpGet]
      public IActionResult Get()
      {
            return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
      }

      [HttpGet("/login")]
      [AllowAnonymous]
      public async Task<IActionResult> Login(string? redirectUri)
      {

            redirectUri = "https://localhost:5002/home/index";
            var authenticationProperties = new AuthenticationProperties
            {
                  RedirectUri = redirectUri
            };
            return Challenge(authenticationProperties, OpenIdConnectDefaults.AuthenticationScheme);
      }
      [HttpGet("/GetUserInfo")]
      public async Task<IActionResult> GetUser(){
            var client = new HttpClient();
            client.SetBearerToken((await _tokenService.GetToken("api")).AccessToken);
            var result = await client.GetAsync("https://localhost:5001/connect/userinfo");
            return Ok(result.Content);
            
      }
}
