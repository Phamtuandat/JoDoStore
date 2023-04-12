using AutoMapper;
using Backend.Extentions;
using gearshop_dotnetapp.Enums;
using gearshop_dotnetapp.Models.Identity;
using gearshop_dotnetapp.Resources;
using gearshop_dotnetapp.Services.EmailService;
using MailKit;
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
        private readonly IEmailService _emailService;
        private readonly IWebHostEnvironment _env;

        public UserController(UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper, RoleManager<IdentityRole> roleManager, AuthenticatorTokenProvider<User> authenticatorTokenProvider, IEmailService emailService, IWebHostEnvironment env)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _roleManager = roleManager;
            _emailService = emailService;
            _env = env;
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
            var roleName = "Customer";
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
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var confirmationLink = string.Empty;
                if (_env.IsDevelopment())
                {
                    confirmationLink = Url.Action("ConfirmEmail", "User",
                        new { userId = user.Id, token },
                        protocol: HttpContext.Request.Scheme);
                }
                else
                {
                    confirmationLink = Url.Action
                       (
                           "ConfirmEmail", "User",
                           new { userId = user.Id, token },
                           protocol: "https",
                           host: "phamtuandat.click"
                       );
                }
                if (confirmationLink != null)
                {
                    await _emailService.SendEmailAsync(new ConfirmEmailRequest() { ToEmail = user.Email, UserName = user.Email, ComfirmEmailLink = confirmationLink });
                }
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
            if (user == null)
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
        [HttpPost("confirmReq")]
        public async Task<IActionResult> SendWelcomeMail()
        {
            var userContext = HttpContext.User;
            var user = await _userManager.GetUserAsync(userContext);
            if (user == null)
            {
                await _signInManager.SignOutAsync();
                return Unauthorized();
            }
            if (_signInManager.IsSignedIn(userContext))
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                var confirmationLink = string.Empty;
                if (_env.IsDevelopment())
                {
                    confirmationLink = Url.Action("ConfirmEmail", "User",
                        new { userId = user.Id, token },
                        protocol: HttpContext.Request.Scheme);
                }
                else
                {
                    confirmationLink = Url.Action
                       (
                           "ConfirmEmail", "User",
                           new { userId = user.Id, token },
                           protocol: "https",
                           host: "phamtuandat.click"
                       );
                }
                if (confirmationLink != null)
                {
                    await _emailService.SendEmailAsync(new ConfirmEmailRequest() { ToEmail = user.Email, UserName = user.Email, ComfirmEmailLink = confirmationLink });
                    return Ok();
                }
                return BadRequest();
            }
            return Unauthorized();
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token))
            {
                return BadRequest("User ID or token is missing.");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return BadRequest("User not found.");
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                string redirectUrl;
                await _signInManager.SignInAsync(user, isPersistent: true);
                if (_env.IsDevelopment())
                {
                    redirectUrl = "http://localhost:3000/";
                }
                else
                {
                    redirectUrl = "https://phamtuandat.click/user";
                }
                return Redirect(redirectUrl);
            }

            return BadRequest("Email confirmation failed.");
        }

    }
}
