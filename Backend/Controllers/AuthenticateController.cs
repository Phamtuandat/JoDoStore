using Backend.Services.Identity;
using Microsoft.AspNetCore.Mvc;
using Backend.Extentions;
using AutoMapper;
using Backend.Models.Identity;
using Backend.Resources;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IIdentitySevice _identityService;
        private readonly IMapper _mapper;
        public AuthenticateController(IIdentitySevice identityService, IMapper mapper)
        {
            _identityService = identityService;
            _mapper = mapper;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.GetErrorMessages());
            var result = await _identityService.RegisterAsync(model);
            if (!result.Success) return BadRequest(result.Message);
            var resource = _mapper.Map<User, UserResource>(result.User);
            var response = new AuthenticatResponse(resource, result.Token, result.RefreshToken);
            return Ok(response);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] AuthenticateRequest model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.GetErrorMessages());
            var result = await _identityService.AuthenticateAsync(model);
            if (!result.Success) return BadRequest(result.Message);
            var resource = _mapper.Map<User, UserResource>(result.User);
            var response = new AuthenticatResponse(resource, result.Token, result.RefreshToken);
            return Ok(response);
        }
        [HttpPost("refreshToken")]
        public async Task<IActionResult> GetUerByToken()
        {
            var user = GetCurrentUser();
            var result = await _identityService.GetUserByToken(user.Email);
            var resource = _mapper.Map<User, UserResource>(result.User);
            var response = new AuthenticatResponse(resource, result.Token, result.RefreshToken);
            return Ok(response);
        }
        private User? GetCurrentUser()
        {
            ClaimsIdentity? idenity = HttpContext.User.Identity as ClaimsIdentity;

            if (idenity != null)
            {
                var userClaims = idenity.Claims;
                return new User
                {
                    Email = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value,
                    FirstName = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.GivenName)?.Value,
                    LastName = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value,
                    Role = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value
                };
            }
            return null;
        }
    }
}
