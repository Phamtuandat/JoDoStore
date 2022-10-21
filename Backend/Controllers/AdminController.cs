using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAdminPage()
        {
            return Ok("You are admin!");
        }




        /*private User? GetCurrentUser()
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
        }*/
    }
}
