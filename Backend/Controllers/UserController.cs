using System.Text;
using System.Text.Encodings.Web;
using App.Data;
using App.Dtos;
using App.Enums;
using App.Models;
using App.Models.Identity;
using App.Services.EmailService;
using AutoMapper;
using Backend.Extentions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;

namespace App.Controllers
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
            private readonly ILogger<UserController> _logger;

            public UserController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager, IMapper mapper, IEmailService emailService, IWebHostEnvironment env, ILogger<UserController> logger)
            {
                  _userManager = userManager;
                  _signInManager = signInManager;
                  _roleManager = roleManager;
                  _mapper = mapper;
                  _emailService = emailService;
                  _env = env;
                  _logger = logger;
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
            public async Task<ActionResult> EditAsync(EditUserResource model)
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

            [HttpPost("register")]
            public async Task<ActionResult> Register(RegisterModel model)
            {
                  if (!ModelState.IsValid)
                  {
                        return Unauthorized(ModelState.GetErrorMessages());
                  }
                  if (model.Email == "phamtuandat1a0@gmail.com")
                  {
                        var userAdmin = await _userManager.FindByEmailAsync("phamtuandat1a0@gmail.com");
                        if (userAdmin == null)
                        {
                              try
                              {
                                    var admin = new User()
                                    {
                                          UserName = "admin",
                                          Email = "phamtuandat1a0@gmail.com",
                                          EmailConfirmed = true
                                    };
                                    await _userManager.CreateAsync(admin, model.Password);
                                    if (_roleManager.FindByNameAsync(RoleNames.Administrator) == null)
                                    {
                                          await _roleManager.CreateAsync(new IdentityRole(RoleNames.Administrator));
                                    }
                                    await _userManager.AddToRoleAsync(admin, RoleNames.Administrator);
                                    return LocalRedirect("https://diydevblog.com");
                              }
                              catch (Exception ex)
                              {
                                    throw new DbUpdateException(ex.Message);
                              }
                        }
                  }
                  else
                  {
                        var user = await _userManager.FindByEmailAsync(model.Email);
                        if (user != null) return Unauthorized("Email has already existed!");
                        user = _mapper.Map<RegisterModel, User>(model);
                        var result = await _userManager.CreateAsync(user, model.Password);
                        if (result.Succeeded)
                        {
                              var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                              code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                              // https://localhost:5001/confirm-email?userId=fdsfds&code=xyz&returnUrl=
                              var callbackUrl = Url.Action
                              (
                                    "ConfirmEmail", "Account",
                                    new { area = "Identity", userId = user.Id, code = code },
                                    protocol: "https",
                                    host: "account.diyDevBlog.com/ConfirmEmail"
                              );

                              await _emailService.SendEmailConfirm(model.Email,
                                    "Confirm email", @$"Your email is existed, please 
                                          <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>Click here</a> to comfirm your email.");
                              if (_userManager.Options.SignIn.RequireConfirmedAccount)
                              {
                                    return Redirect("https://account.diydevblog.com/RegisterConfirmation");
                              }
                              else
                              {
                                    await _signInManager.SignInAsync(user, isPersistent: false);
                                    return Redirect("https://diydevblog.com");
                              }
                        }
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

      }
}