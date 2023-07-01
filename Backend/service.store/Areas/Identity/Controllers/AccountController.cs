// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using App.Utilities;
using App.Areas.Identity.Models.AccountViewModels;
using App.Services.EmailService;
using App.Data;
using Microsoft.EntityFrameworkCore;
using App.Models.Identity;

namespace App.Areas.Identity.Controllers
{

      [ApiExplorerSettings(IgnoreApi = true)]
      [Area("Identity")]
      [Route("Account/[action]")]
      public class AccountController : Controller
      {
            private readonly UserManager<User> _userManager;
            private readonly SignInManager<User> _signInManager;
            private readonly IEmailService _emailSender;
            private readonly ILogger<AccountController> _logger;
            private readonly RoleManager<IdentityRole> _roleManager;

            public AccountController(
                UserManager<User> userManager,
                SignInManager<User> signInManager,
                IEmailService emailSender,
                ILogger<AccountController> logger, RoleManager<IdentityRole> roleManager)
            {
                  _userManager = userManager;
                  _signInManager = signInManager;
                  _emailSender = emailSender;
                  _logger = logger;
                  _roleManager = roleManager;
            }

            // GET: /Account/Login
            [HttpGet]
            [AllowAnonymous]
            public IActionResult Login(string? returnUrl = null)
            {
                  ViewData["ReturnUrl"] = returnUrl;
                  var user = HttpContext.User;
                  if (_signInManager.IsSignedIn(user))
                  {
                        return RedirectToAction("Index", "Admin", new { area = "Admin" });
                  }
                  return View();
            }

            //
            // POST: /Account/Login
            [HttpPost]
            [AllowAnonymous]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
            {
                  returnUrl ??= Url.Content("~/");
                  ViewData["ReturnUrl"] = returnUrl;
                  if (ModelState.IsValid)
                  {
                        var result = await _signInManager.PasswordSignInAsync(model.UserNameOrEmail, model.Password, model.RememberMe, lockoutOnFailure: true);


                        if ((!result.Succeeded) && AppUtilities.IsValidEmail(model.UserNameOrEmail))
                        {
                              var user = await _userManager.FindByEmailAsync(model.UserNameOrEmail);
                              if (user != null && user.UserName != null)
                              {
                                    result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, model.RememberMe, lockoutOnFailure: true);
                              }
                        }
                        if (result.Succeeded)
                        {
                              _logger.LogInformation(1, "User logged in.");
                              return RedirectToAction("Index", "Admin", new { area = "Admin" });
                        }
                        if (result.RequiresTwoFactor)
                        {
                              return RedirectToAction(nameof(SendCode), new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                        }

                        if (result.IsLockedOut)
                        {
                              _logger.LogWarning(2, "Your account is locked");
                              return View("Lockout");
                        }
                        else
                        {
                              ModelState.AddModelError("can not signin.");
                              return View(model);
                        }
                  }
                  return View(model);
            }
            // GET: /Account/Register
            [HttpGet]
            [AllowAnonymous]
            public IActionResult Register(string? returnUrl = null)
            {
                  var user = HttpContext.User;
                  if (_signInManager.IsSignedIn(user))
                  {
                        return RedirectToAction("Index", "Home", new { area = "" });
                  }
                  returnUrl ??= Url.Content("~/");
                  ViewData["ReturnUrl"] = returnUrl;
                  return View();
            }
            //
            // POST: /Account/Register
            [HttpPost]
            [AllowAnonymous]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Register(RegisterViewModel model, string? returnUrl = null)
            {
                  returnUrl ??= Url.Content("~/");
                  ViewData["ReturnUrl"] = returnUrl;
                  if (ModelState.IsValid)
                  {

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
                                          await _userManager.AddToRoleAsync(admin, RoleNames.Administrator);
                                    }
                                    catch (Exception ex)
                                    {
                                          throw new DbUpdateException(ex.Message);
                                    }
                              }
                        }
                        else
                        {
                              var user = new User { UserName = model.UserName, Email = model.Email, LastName = model.LastName, FirstName = model.FirstName };
                              var result = await _userManager.CreateAsync(user, model.Password);
                              if (result.Succeeded)
                              {
                                    _logger.LogInformation("New user is created.");


                                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                                    // https://localhost:5001/confirm-email?userId=fdsfds&code=xyz&returnUrl=
                                    var callbackUrl = Url.ActionLink(
                                          action: nameof(ConfirmEmail),
                                          values:
                                                new
                                                {
                                                      area = "Identity",
                                                      userId = user.Id,
                                                      code = code
                                                },
                                          protocol: Request.Scheme);
                                    await _emailSender.SendEmailConfirm(model.Email,
                                          "Confirm email", @$"Your email is existed, please 
                                          <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>Click here</a> to comfirm your email.");
                                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                                    {
                                          return LocalRedirect(Url.Action(nameof(RegisterConfirmation)));
                                    }
                                    else
                                    {
                                          await _signInManager.SignInAsync(user, isPersistent: false);
                                          return LocalRedirect(returnUrl);
                                    }
                              }
                              ModelState.AddModelError(result);
                        }
                  }

                  // If we got this far, something failed, redisplay form
                  return View(model);
            }
            // GET: /Account/ConfirmEmail
            [HttpGet]
            [AllowAnonymous]
            public ActionResult RegisterConfirmation()
            {

                  return View();
            }

            // GET: /Account/ConfirmEmail
            [HttpGet]
            [AllowAnonymous]
            public async Task<IActionResult> ConfirmEmail(string userId, string code)
            {
                  if (userId == null || code == null)
                  {
                        return View("ErrorConfirmEmail");
                  }
                  var user = await _userManager.FindByIdAsync(userId);
                  if (user == null)
                  {
                        return View("ErrorConfirmEmail");
                  }
                  code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
                  var result = await _userManager.ConfirmEmailAsync(user, code);
                  if (_roleManager.FindByNameAsync(RoleNames.Administrator) == null)
                  {
                        await _roleManager.CreateAsync(new IdentityRole(RoleNames.Member));
                  }
                  await _userManager.AddToRoleAsync(user, RoleNames.Member);
                  return LocalRedirect("https://diydevblog.com/");
            }


            // POST: /Account/LogOff
            [HttpPost("/logout/")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> LogOff()
            {
                  await _signInManager.SignOutAsync();
                  _logger.LogInformation("User Logout");
                  return RedirectToAction("Login", "Account", new { area = "Identity" });
            }

            // POST: /Account/ExternalLogin
            [HttpPost]
            [AllowAnonymous]
            [ValidateAntiForgeryToken]
            public IActionResult ExternalLogin(string provider, string returnUrl = null)
            {
                  returnUrl ??= Url.Content("~/");
                  var redirectUrl = Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl });
                  var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
                  return Challenge(properties, provider);
            }
            //
            // GET: /Account/ExternalLoginCallback
            [HttpGet]
            [AllowAnonymous]
            public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
            {
                  returnUrl ??= Url.Content("~/");
                  if (remoteError != null)
                  {
                        ModelState.AddModelError(string.Empty, $"Extenal service error: {remoteError}");
                        return View(nameof(Login));
                  }
                  var info = await _signInManager.GetExternalLoginInfoAsync();
                  if (info == null)
                  {
                        return RedirectToAction(nameof(Login));
                  }

                  // Sign in the user with this external login provider if the user already has a login.
                  var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false);
                  if (result.Succeeded)
                  {
                        // Cập nhật lại token
                        await _signInManager.UpdateExternalAuthenticationTokensAsync(info);

                        _logger.LogInformation(5, "User logged in with {Name} provider.", info.LoginProvider);
                        return LocalRedirect(returnUrl);
                  }
                  if (result.RequiresTwoFactor)
                  {
                        return RedirectToAction(nameof(SendCode), new { ReturnUrl = returnUrl });
                  }
                  if (result.IsLockedOut)
                  {
                        return View("Lockout");
                  }
                  else
                  {
                        // If the user does not have an account, then ask the user to create an account.
                        ViewData["ReturnUrl"] = returnUrl;
                        ViewData["ProviderDisplayName"] = info.ProviderDisplayName;
                        var email = info.Principal.FindFirstValue(ClaimTypes.Email);
                        return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = email });
                  }
            }

            //
            // POST: /Account/ExternalLoginConfirmation
            [HttpPost]
            [AllowAnonymous]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl = null)
            {
                  returnUrl ??= Url.Content("~/");
                  if (ModelState.IsValid)
                  {
                        // Get the information about the user from the external login provider
                        var info = await _signInManager.GetExternalLoginInfoAsync();
                        if (info == null)
                        {
                              return View("ExternalLoginFailure");
                        }

                        // Input.Email
                        var registeredUser = await _userManager.FindByEmailAsync(model.Email);
                        string externalEmail = null;
                        User externalEmailUser = null;

                        // Claim ~ Dac tinh mo ta mot doi tuong 
                        if (info.Principal.HasClaim(c => c.Type == ClaimTypes.Email))
                        {
                              externalEmail = info.Principal.FindFirstValue(ClaimTypes.Email);
                        }

                        if (externalEmail != null)
                        {
                              externalEmailUser = await _userManager.FindByEmailAsync(externalEmail);
                        }

                        if ((registeredUser != null) && (externalEmailUser != null))
                        {
                              // externalEmail  == Input.Email
                              if (registeredUser.Id == externalEmailUser.Id)
                              {
                                    // Lien ket tai khoan, dang nhap
                                    var resultLink = await _userManager.AddLoginAsync(registeredUser, info);
                                    if (resultLink.Succeeded)
                                    {
                                          await _signInManager.SignInAsync(registeredUser, isPersistent: false);
                                          return LocalRedirect(returnUrl);
                                    }
                              }
                              else
                              {
                                    // registeredUser = externalEmailUser (externalEmail != Input.Email)
                                    /*
                                        info => user1 (mail1@abc.com)
                                             => user2 (mail2@abc.com)
                                    */
                                    ModelState.AddModelError(string.Empty, "Something went wrong. please try again later!");
                                    return View();
                              }
                        }


                        if ((externalEmailUser != null) && (registeredUser == null))
                        {
                              ModelState.AddModelError(string.Empty);
                              return View();
                        }

                        if ((externalEmailUser == null) && (externalEmail == model.Email))
                        {
                              // Chua co Account -> Tao Account, lien ket, dang nhap
                              var newUser = new User()
                              {
                                    UserName = externalEmail,
                                    Email = externalEmail
                              };

                              var resultNewUser = await _userManager.CreateAsync(newUser);
                              if (resultNewUser.Succeeded)
                              {
                                    await _userManager.AddLoginAsync(newUser, info);
                                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
                                    await _userManager.ConfirmEmailAsync(newUser, code);

                                    await _signInManager.SignInAsync(newUser, isPersistent: false);

                                    return LocalRedirect(returnUrl);

                              }
                              else
                              {
                                    ModelState.AddModelError("Something went wrong. please try again later!");
                                    return View();
                              }
                        }


                        var user = new User { UserName = model.Email, Email = model.Email };
                        var result = await _userManager.CreateAsync(user);
                        if (result.Succeeded)
                        {
                              result = await _userManager.AddLoginAsync(user, info);
                              if (result.Succeeded)
                              {
                                    await _signInManager.SignInAsync(user, isPersistent: false);
                                    _logger.LogInformation(6, "User created an account using {Name} provider.", info.LoginProvider);

                                    // Update any authentication tokens as well
                                    await _signInManager.UpdateExternalAuthenticationTokensAsync(info);

                                    return LocalRedirect(returnUrl);
                              }
                        }
                        ModelState.AddModelError(result);
                  }

                  ViewData["ReturnUrl"] = returnUrl;
                  return View(model);
            }

            //
            // GET: /Account/ForgotPassword
            [HttpGet]
            [AllowAnonymous]
            public IActionResult ForgotPassword()
            {
                  return View();
            }

            //
            // POST: /Account/ForgotPassword
            [HttpPost]
            [AllowAnonymous]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
            {
                  if (ModelState.IsValid)
                  {
                        var user = await _userManager.FindByEmailAsync(model.Email);
                        if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                        {
                              // Don't reveal that the user does not exist or is not confirmed
                              return View("ForgotPasswordConfirmation");
                        }
                        var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                        var callbackUrl = Url.ActionLink(
                            action: nameof(ResetPassword),
                            values: new { area = "Identity", code },
                            protocol: Request.Scheme);


                        await _emailSender.SendEmailConfirm(
                            model.Email,
                            "Reset Password",
                            $"Please <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>Click here</a> to reset password.");

                        return RedirectToAction(nameof(ForgotPasswordConfirmation));



                  }
                  return View(model);
            }

            //
            // GET: /Account/ForgotPasswordConfirmation
            [HttpGet]
            [AllowAnonymous]
            public IActionResult ForgotPasswordConfirmation()
            {
                  return View();
            }

            //
            // GET: /Account/ResetPassword
            [HttpGet]
            [AllowAnonymous]
            public IActionResult ResetPassword(string code = null)
            {
                  return code == null ? View("Error") : View();
            }

            //
            // POST: /Account/ResetPassword
            [HttpPost]
            [AllowAnonymous]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
            {
                  if (!ModelState.IsValid)
                  {
                        return View(model);
                  }
                  var user = await _userManager.FindByEmailAsync(model.Email);
                  if (user == null)
                  {
                        return RedirectToAction(nameof(AccountController.ResetPasswordConfirmation), "Account");
                  }
                  var code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(model.Code));

                  var result = await _userManager.ResetPasswordAsync(user, code, model.Password);
                  if (result.Succeeded)
                  {
                        return RedirectToAction(nameof(AccountController.ResetPasswordConfirmation), "Account");
                  }
                  ModelState.AddModelError(result);
                  return View();
            }

            //
            // GET: /Account/ResetPasswordConfirmation
            [HttpGet]
            [AllowAnonymous]
            public IActionResult ResetPasswordConfirmation()
            {
                  return View();
            }

            //
            // GET: /Account/SendCode
            [HttpGet]
            [AllowAnonymous]
            public async Task<ActionResult> SendCode(string returnUrl = null, bool rememberMe = false)
            {
                  var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
                  if (user == null)
                  {
                        return View("Error");
                  }
                  var userFactors = await _userManager.GetValidTwoFactorProvidersAsync(user);
                  var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
                  return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
            }
            //
            // POST: /Account/SendCode
            [HttpPost]
            [AllowAnonymous]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> SendCode(SendCodeViewModel model)
            {
                  if (!ModelState.IsValid)
                  {
                        return View();
                  }

                  var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
                  if (user == null)
                  {
                        return View("Error");
                  }
                  // Dùng mã Authenticator
                  if (model.SelectedProvider == "Authenticator")
                  {
                        return RedirectToAction(nameof(VerifyAuthenticatorCode), new { ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
                  }

                  // Generate the token and send it
                  var code = await _userManager.GenerateTwoFactorTokenAsync(user, model.SelectedProvider);
                  if (string.IsNullOrWhiteSpace(code))
                  {
                        return View("Error");
                  }

                  var message = "Your security code is: " + code;
                  if (model.SelectedProvider == "Email")
                  {
                        await _emailSender.SendEmailConfirm(await _userManager.GetEmailAsync(user), "Security Code", message);
                  }
                  else if (model.SelectedProvider == "Phone")
                  {
                        await _emailSender.SendSmsAsync(await _userManager.GetPhoneNumberAsync(user), message);
                  }

                  return RedirectToAction(nameof(VerifyCode), new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
            }
            //
            // GET: /Account/VerifyCode
            [HttpGet]
            [AllowAnonymous]
            public async Task<IActionResult> VerifyCode(string provider, bool rememberMe, string returnUrl = null)
            {
                  // Require that the user has already logged in via username/password or external login
                  var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
                  if (user == null)
                  {
                        return View("Error");
                  }
                  return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
            }

            //
            // POST: /Account/VerifyCode
            [HttpPost]
            [AllowAnonymous]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> VerifyCode(VerifyCodeViewModel model)
            {
                  model.ReturnUrl ??= Url.Content("~/");
                  if (!ModelState.IsValid)
                  {
                        return View(model);
                  }

                  // The following code protects for brute force attacks against the two factor codes.
                  // If a user enters incorrect codes for a specified amount of time then the user account
                  // will be locked out for a specified amount of time.
                  var result = await _signInManager.TwoFactorSignInAsync(model.Provider, model.Code, model.RememberMe, model.RememberBrowser);
                  if (result.Succeeded)
                  {
                        return LocalRedirect(model.ReturnUrl);
                  }
                  if (result.IsLockedOut)
                  {
                        _logger.LogWarning(7, "User account locked out.");
                        return View("Lockout");
                  }
                  else
                  {
                        ModelState.AddModelError(string.Empty, "Invalid code.");
                        return View(model);
                  }
            }

            //
            // GET: /Account/VerifyAuthenticatorCode
            [HttpGet]
            [AllowAnonymous]
            public async Task<IActionResult> VerifyAuthenticatorCode(bool rememberMe, string returnUrl = null)
            {
                  // Require that the user has already logged in via username/password or external login
                  var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
                  if (user == null)
                  {
                        return View("Error");
                  }
                  return View(new VerifyAuthenticatorCodeViewModel { ReturnUrl = returnUrl, RememberMe = rememberMe });
            }

            //
            // POST: /Account/VerifyAuthenticatorCode
            [HttpPost]
            [AllowAnonymous]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> VerifyAuthenticatorCode(VerifyAuthenticatorCodeViewModel model)
            {
                  model.ReturnUrl ??= Url.Content("~/");
                  if (!ModelState.IsValid)
                  {
                        return View(model);
                  }

                  // The following code protects for brute force attacks against the two factor codes.
                  // If a user enters incorrect codes for a specified amount of time then the user account
                  // will be locked out for a specified amount of time.
                  var result = await _signInManager.TwoFactorAuthenticatorSignInAsync(model.Code, model.RememberMe, model.RememberBrowser);
                  if (result.Succeeded)
                  {
                        return LocalRedirect(model.ReturnUrl);
                  }
                  if (result.IsLockedOut)
                  {
                        _logger.LogWarning(7, "User account locked out.");
                        return View("Lockout");
                  }
                  else
                  {
                        ModelState.AddModelError(string.Empty, "Mã sai.");
                        return View(model);
                  }
            }
            //
            // GET: /Account/UseRecoveryCode
            [HttpGet]
            [AllowAnonymous]
            public async Task<IActionResult> UseRecoveryCode(string returnUrl = null)
            {
                  // Require that the user has already logged in via username/password or external login
                  var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
                  if (user == null)
                  {
                        return View("Error");
                  }
                  return View(new UseRecoveryCodeViewModel { ReturnUrl = returnUrl });
            }

            //
            // POST: /Account/UseRecoveryCode
            [HttpPost]
            [AllowAnonymous]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> UseRecoveryCode(UseRecoveryCodeViewModel model)
            {
                  model.ReturnUrl ??= Url.Content("~/");
                  if (!ModelState.IsValid)
                  {
                        return View(model);
                  }

                  var result = await _signInManager.TwoFactorRecoveryCodeSignInAsync(model.Code);
                  if (result.Succeeded)
                  {
                        return LocalRedirect(model.ReturnUrl);
                  }
                  else
                  {
                        ModelState.AddModelError(string.Empty, "restore code is invalid.");
                        return View(model);
                  }
            }

            [AllowAnonymous]
            public IActionResult AccessDenied()
            {
                  return View();
            }
      }
}

