﻿using AutoMapper;
using Backend.Extentions;
using gearshop_dotnetapp.Extensions;
using gearshop_dotnetapp.Models.Identity;
using gearshop_dotnetapp.Resources;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace gearshop_dotnetapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediatr;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;

        public UserController(IMediator mediatr, UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper)
        {
            _mediatr = mediatr;
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return Unauthorized(ModelState.GetErrorMessages());
            }
            var user = await _userManager.FindByEmailAsync(userModel.Email);
            if(user != null && await _userManager.CheckPasswordAsync(user, userModel.Password)) 
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                var userResource = _mapper.Map<User, UserResource>(user);
                return Ok(userResource);
            }
            return BadRequest("Email or password is invalid!");
        }



        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterResource model)
        {
            if (!ModelState.IsValid)
            {
                return Unauthorized(ModelState.GetErrorMessages());
            }
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null) return Unauthorized("Email has already existed!");
            user = _mapper.Map<RegisterResource, User>(model);
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                var userResource = _mapper.Map<User, UserResource>(user);
                return Ok(userResource);
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return Unauthorized(ModelState.GetErrorMessages());
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
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
            if(user == null) return Unauthorized();
            if (_signInManager.IsSignedIn(userContext))
            {
                await _signInManager.RefreshSignInAsync(user);
                return Ok(_mapper.Map<User, UserResource>(user));
            }
            return Unauthorized();
        }
    }
}
