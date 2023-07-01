using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{

      [Route("api/[controller]")]
      [ApiController]
      public class ImageController : ControllerBase
      {
            private readonly IHostEnvironment _env;
            

            public ImageController(IHostEnvironment env)
            {
                  _env = env;
            }

            [HttpGet("/api/banner")]
            public async Task<ActionResult> GetBannerPath()
            {
                  List<string> imageNamesList = new List<string>();
                  string imageDirectoryPath = string.Empty;
                  string prefix = string.Empty;
                  if (_env.IsDevelopment())
                  {
                        prefix = "https://localhost:7243/Contents/Image/Banner/";
                        imageDirectoryPath  = "Uploads/Image/Banner";
                  }
                  else
                  {
                        imageDirectoryPath = "Uploads/Image/Banner";
                        prefix = "https://static.diydevblog.com/Image/Banner/";
                  }
                  string[] imagePaths = Directory.GetFiles(imageDirectoryPath)
                        .ToArray();
                  foreach (string imagePath in imagePaths)
                  {
                        string imageName = Path.GetFileName(imagePath);
                        imageNamesList.Add(prefix + imageName);
                  }
                  var serializedData = JsonSerializer.Serialize(imageNamesList);
                  return Ok(imageNamesList);
            }
      }
}