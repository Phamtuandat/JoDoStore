// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Security.Claims;
using App.Areas.Identity.Models.RoleViewModels;
using App.Data;
using App.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App.Areas.Identity.Controllers
{
      [ApiExplorerSettings(IgnoreApi = true)]

      [Area("Identity")]
      [Route("/Role/[action]")]
      [Authorize(Roles = RoleNames.Administrator)]

      public class RoleController : Controller
      {

            private readonly ILogger<RoleController> _logger;
            private readonly RoleManager<IdentityRole> _roleManager;
            private readonly DataContext _context;

            private readonly UserManager<User> _userManager;

            public RoleController(ILogger<RoleController> logger, RoleManager<IdentityRole> roleManager, DataContext context, UserManager<User> userManager)
            {
                  _logger = logger;
                  _roleManager = roleManager;
                  _context = context;
                  _userManager = userManager;
            }

            [TempData]
            public string Message { get; set; } = string.Empty;

            //
            // GET: /Role/Index
            [HttpGet]
            public async Task<IActionResult> Index()
            {

                  var r = await _roleManager.Roles.OrderBy(r => r.Name).ToListAsync();
                  var roles = new List<RoleModel>();
                  foreach (var _r in r)
                  {
                        var claims = await _roleManager.GetClaimsAsync(_r);
                        var claimsString = claims.Select(c => c.Type + "=" + c.Value);

                        var rm = new RoleModel()
                        {
                              Name = _r.Name,
                              Id = _r.Id,
                              Claims = claimsString.ToArray()
                        };
                        roles.Add(rm);
                  }

                  return View(roles);
            }

            // GET: /Role/Create
            [HttpGet]
            public IActionResult Create()
            {
                  return View();
            }

            // POST: /Role/Create
            [HttpPost, ActionName(nameof(Create))]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> CreateAsync(CreateRoleModel model)
            {
                  if (!ModelState.IsValid)
                  {
                        return View();
                  }

                  var newRole = new IdentityRole(model.Name);
                  var result = await _roleManager.CreateAsync(newRole);
                  if (result.Succeeded)
                  {
                        Message = $"Create role successfully: {model.Name}";
                        return RedirectToAction(nameof(Index));
                  }
                  else
                  {
                        ModelState.AddModelError(result);
                  }
                  return View();
            }

            // GET: /Role/Delete/roleid
            [HttpGet("{roleid}")]
            public async Task<IActionResult> DeleteAsync(string roleid)
            {
                  if (roleid == null) return NotFound();
                  var role = await _roleManager.FindByIdAsync(roleid);
                  if (role == null)
                  {
                        return NotFound();
                  }
                  return View(role);
            }

            // POST: /Role/Edit/1
            [HttpPost("{roleid}"), ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmAsync(string roleid)
            {
                  if (roleid == null) return NotFound();
                  var role = await _roleManager.FindByIdAsync(roleid);
                  if (role == null) return NotFound();

                  var result = await _roleManager.DeleteAsync(role);

                  if (result.Succeeded)
                  {
                        Message = $"Deleted: {role.Name}";
                        return RedirectToAction(nameof(Index));
                  }
                  else
                  {
                        ModelState.AddModelError(result);
                  }
                  return View(role);
            }

            // GET: /Role/Edit/roleid
            [HttpGet("{roleid}")]
            public async Task<IActionResult> EditAsync(string roleid, [Bind("Name")] EditRoleModel model)
            {
                  if (roleid == null) return NotFound();
                  var role = await _roleManager.FindByIdAsync(roleid);
                  if (role == null)
                  {
                        return NotFound();
                  }
                  model.Name = role.Name;
                  model.Claims = await _context.RoleClaims.Where(rc => rc.RoleId == role.Id).ToListAsync();
                  model.Role = role;
                  ModelState.Clear();
                  return View(model);

            }

            // POST: /Role/Edit/1
            [HttpPost("{roleid}"), ActionName("Edit")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> EditConfirmAsync(string roleid, [Bind("Name")] EditRoleModel model)
            {
                  if (roleid == null) return NotFound();
                  var role = await _roleManager.FindByIdAsync(roleid);
                  if (role == null)
                  {
                        return NotFound();
                  }
                  model.Claims = await _context.RoleClaims.Where(rc => rc.RoleId == role.Id).ToListAsync();
                  model.Role = role;
                  if (!ModelState.IsValid)
                  {
                        return View(model);
                  }

                  role.Name = model.Name;
                  var result = await _roleManager.UpdateAsync(role);

                  if (result.Succeeded)
                  {
                        Message = $"You just renamed: {model.Name}";
                        return RedirectToAction(nameof(Index));
                  }
                  else
                  {
                        ModelState.AddModelError(result);
                  }

                  return View(model);
            }

            // GET: /Role/AddRoleClaim/roleid
            [HttpGet("{roleid}")]
            public async Task<IActionResult> AddRoleClaimAsync(string roleid)
            {
                  if (roleid == null) return
                  NotFound();
                  var role = await _roleManager.FindByIdAsync(roleid);
                  if (role == null)
                  {
                        return NotFound();
                  }

                  var model = new EditClaimModel()
                  {
                        role = role
                  };
                  return View(model);
            }

            // POST: /Role/AddRoleClaim/roleid
            [HttpPost("{roleid}")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> AddRoleClaimAsync(string roleid, [Bind("ClaimType", "ClaimValue")] EditClaimModel model)
            {
                  if (roleid == null) return NotFound();
                  var role = await _roleManager.FindByIdAsync(roleid);
                  if (role == null)
                  {
                        return NotFound();
                  }
                  model.role = role;
                  if (!ModelState.IsValid) return View(model);


                  if ((await _roleManager.GetClaimsAsync(role)).Any(c => c.Type == model.ClaimType && c.Value == model.ClaimValue))
                  {
                        ModelState.AddModelError(string.Empty, "This Claim already existed in role");
                        return View(model);
                  }

                  var newClaim = new Claim(model.ClaimType, model.ClaimValue);
                  var result = await _roleManager.AddClaimAsync(role, newClaim);

                  if (!result.Succeeded)
                  {
                        ModelState.AddModelError(result);
                        return View(model);
                  }

                  Message = "A new claim is created successfully";

                  return RedirectToAction("Edit", new { roleid = role.Id });

            }

            // GET: /Role/EditRoleClaim/claimid
            [HttpGet("{claimid:int}")]
            public async Task<IActionResult> EditRoleClaim(int claimid)
            {
                  var claim = _context.RoleClaims.Where(c => c.Id == claimid).FirstOrDefault();
                  if (claim == null) return NotFound();

                  var role = await _roleManager.FindByIdAsync(claim.RoleId);
                  if (role == null) return NotFound();
                  ViewBag.claimid = claimid;

                  var Input = new EditClaimModel()
                  {
                        ClaimType = claim.ClaimType,
                        ClaimValue = claim.ClaimValue,
                        role = role
                  };


                  return View(Input);
            }

            // GET: /Role/EditRoleClaim/claimid
            [HttpPost("{claimid:int}")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> EditRoleClaim(int claimid, [Bind("ClaimType", "ClaimValue")] EditClaimModel Input)
            {
                  var claim = _context.RoleClaims.Where(c => c.Id == claimid).FirstOrDefault();
                  if (claim == null) return NotFound();

                  ViewBag.claimid = claimid;

                  var role = await _roleManager.FindByIdAsync(claim.RoleId);
                  if (role == null) return NotFound();
                  Input.role = role;
                  if (!ModelState.IsValid)
                  {
                        return View(Input);
                  }
                  if (_context.RoleClaims.Any(c => c.RoleId == role.Id && c.ClaimType == Input.ClaimType && c.ClaimValue == Input.ClaimValue && c.Id != claim.Id))
                  {
                        ModelState.AddModelError(string.Empty, "This claim is already in role");
                        return View(Input);
                  }


                  claim.ClaimType = Input.ClaimType;
                  claim.ClaimValue = Input.ClaimValue;

                  await _context.SaveChangesAsync();

                  Message = "claims is just updated";

                  return RedirectToAction("Edit", new { roleid = role.Id });
            }
            // POST: /Role/EditRoleClaim/claimid
            [HttpPost("{claimid:int}")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteClaim(int claimid, [Bind("ClaimType", "ClaimValue")] EditClaimModel Input)
            {
                  var claim = _context.RoleClaims.Where(c => c.Id == claimid).FirstOrDefault();
                  if (claim == null) return NotFound();

                  var role = await _roleManager.FindByIdAsync(claim.RoleId);
                  if (role == null) return NotFound();
                  Input.role = role;
                  if (!ModelState.IsValid)
                  {
                        return View(Input);
                  }
                  if (_context.RoleClaims.Any(c => c.RoleId == role.Id && c.ClaimType == Input.ClaimType && c.ClaimValue == Input.ClaimValue && c.Id != claim.Id))
                  {
                        ModelState.AddModelError(string.Empty, "This Claim has already been assigned");
                        return View(Input);
                  }


                  await _roleManager.RemoveClaimAsync(role, new Claim(claim.ClaimType, claim.ClaimValue));

                  Message = "Claim has been removed";


                  return RedirectToAction("Edit", new { roleid = role.Id });
            }


      }
}
