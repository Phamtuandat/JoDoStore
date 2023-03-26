using AutoMapper;
using Backend.Extentions;
using gearshop_dotnetapp.Enums;
using gearshop_dotnetapp.Models.Identity;
using gearshop_dotnetapp.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace gearshop_dotnetapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public UserController(UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper, RoleManager<IdentityRole> roleManager, AuthenticatorTokenProvider<User> authenticatorTokenProvider) 
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _roleManager = roleManager;
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] UserLoginModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return Unauthorized(ModelState.GetErrorMessages());
            }
            var user = await _userManager.FindByEmailAsync(userModel.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, userModel.Password))
            {
                await _signInManager.SignInAsync(user, isPersistent: true);
                var userResource = _mapper.Map<User, UserResource>(user);
                return Ok(userResource);
            }
            return BadRequest("Email or password is invalid!");
        }

        [HttpPatch]
        [Authorize]
        public async Task<ActionResult> EditAsync([FromBody] EditUserResource model)
        {
            Gender gender;
            if (!Enum.TryParse(model.Gender, out gender))
            {
                return BadRequest("Invalid gender value.");
            }
            if (!ModelState.IsValid)
            {
                return Unauthorized(ModelState.GetErrorMessages());
            }
            var userContext = HttpContext.User;
            var user = await _userManager.GetUserAsync(userContext);
            if (user == null)
            {
                await _signInManager.SignOutAsync();
                return Unauthorized();
            }
            var dateOnly = DateOnly.FromDateTime(model.Birthday);
            user.Gender = gender;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Birthday = dateOnly;
            user.PhoneNumber = model.PhoneNumber;
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return Ok(_mapper.Map<User, UserResource>(user));
            }
            return BadRequest(result.Errors);
        }
        [HttpPatch("changePassword")]
        [Authorize]
        public async Task<ActionResult> ChangePwAsync([FromBody] ChangePwResource model)
        {
            if (!ModelState.IsValid)
            {
                return Unauthorized(ModelState.GetErrorMessages());
            }
            var userContext = HttpContext.User;
            var user = await _userManager.GetUserAsync(userContext);
            if (user == null)
            {
                await _signInManager.SignOutAsync();
                return Unauthorized();
            }
            var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
            if (result.Succeeded)
            {
                return Ok();
            }
            return BadRequest("password or email is invalid!");
        }


        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterResource model)
        {
            if (!ModelState.IsValid)
            {
                return Unauthorized(ModelState.GetErrorMessages());
            }
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null) return Unauthorized("Email has already existed!");
            user = _mapper.Map<RegisterResource, User>(model);
            var result = await _userManager.CreateAsync(user, model.Password);
            var roleName = "Admin";
            var roleExists = await _roleManager.RoleExistsAsync(roleName);

            if (!roleExists)
            {
                var role = new IdentityRole(roleName);
                await _roleManager.CreateAsync(role);
            }
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: true);
                var userResource = _mapper.Map<User, UserResource>(user);
                await _userManager.AddToRoleAsync(user, roleName);
                return Ok(userResource);
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return Unauthorized(ModelState.GetErrorMessages());
        }

        [HttpPost("logout")]
        public async Task<ActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return StatusCode(202);
        }

        [HttpPost("[action]")]
        [Authorize]
        public async Task<IActionResult> Validate()
        {
            var userContext = HttpContext.User;
            var user = await _userManager.GetUserAsync(userContext);
            if (user == null) return Unauthorized();
            if (_signInManager.IsSignedIn(userContext))
            {
                await _signInManager.RefreshSignInAsync(user);
                return Ok(_mapper.Map<User, UserResource>(user));
            }
            return Unauthorized();
        }

        [HttpPost("[action]")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Admin()
        {
            
            var userContext = HttpContext.User;
            var user = await _userManager.GetUserAsync(userContext);
            if(user == null)
            {
                await _signInManager.SignOutAsync();
                return Unauthorized();
            }
            if (_signInManager.IsSignedIn(userContext))
            {
                await _signInManager.RefreshSignInAsync(user);
                return Ok();
            }
            return Unauthorized();
        }


    }
}
