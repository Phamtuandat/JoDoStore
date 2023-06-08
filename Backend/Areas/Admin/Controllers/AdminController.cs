
using App.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.Areas.Admin.Controllers
{
      [Area("Admin")]
      [Route("/")]
      [Authorize(Roles = RoleNames.Administrator)]
      [ApiExplorerSettings(IgnoreApi = true)]
      public class AdminController : Controller
      {
            // GET: Admin
            public ActionResult Index()
            {
                  return View();
            }
      }
}