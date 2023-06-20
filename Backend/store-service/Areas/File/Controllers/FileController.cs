// using gearshop_dotnetapp.Data;
// using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

using elFinder.NetCore;
using elFinder.NetCore.Drivers.FileSystem;
using Microsoft.AspNetCore.Authorization;
using App.Data;

namespace App.Areas.File.Controllers
{

      [Authorize(Roles = RoleNames.Administrator)]
      [Area("File")]
      [Route("file-manage/[action]")]

      [ApiExplorerSettings(IgnoreApi = true)]
      public class FileController : Controller
      {

            [Route("/file-manager")]
            public IActionResult Index()
            {
                  return View();
            }
            IWebHostEnvironment _env;
            public FileController(IWebHostEnvironment env) => _env = env;

            [Route("/file-manager-connector")]
            public async Task<IActionResult> Connector()
            {
                  var connector = GetConnector();
                  return await connector.ProcessAsync(Request);
            }

            [Route("/file-manager-thumb/{hash}")]
            public async Task<IActionResult> Thumbs(string hash)
            {
                  var connector = GetConnector();
                  return await connector.GetThumbnailAsync(HttpContext.Request, HttpContext.Response, hash);
            }

            private Connector GetConnector()
            {
                  string pathroot = "Uploads";
                  string requestUrl = "Contents";

                  var driver = new FileSystemDriver();

                  string absoluteUrl = UriHelper.BuildAbsolute(Request.Scheme, Request.Host);
                  var uri = new Uri(absoluteUrl);

                  // .. ... wwww/files
                  string rootDirectory = Path.Combine(_env.ContentRootPath, pathroot);
                  if (_env.IsProduction())
                  {
                        rootDirectory = "/app/Uploads";

                  }

                  // https://localhost:5001/files/
                  string url = $"{uri.Scheme}://{uri.Authority}/{requestUrl}/";
                  string urlthumb = $"{uri.Scheme}://{uri.Authority}/file-manager-thumb";


                  var root = new RootVolume(rootDirectory, url, urlthumb)
                  {
                        //IsReadOnly = !User.IsInRole("Administrators")
                        IsReadOnly = false, // Can be readonly according to user's membership permission
                        IsLocked = false, // If locked, files and directories cannot be deleted, renamed or moved
                        Alias = "Files", // Beautiful name given to the root/home folder
                                         //MaxUploadSizeInKb = 2048, // Limit imposed to user uploaded file <= 2048 KB
                                         //LockedFolders = new List<string>(new string[] { "Folder1" }
                        ThumbnailSize = 100,



                  };
                  driver.AddRoot(root);

                  return new Connector(driver)
                  {
                        // This allows support for the "onlyMimes" option on the client.
                        MimeDetect = MimeDetectOption.Internal,

                  };
            }
      }
}
