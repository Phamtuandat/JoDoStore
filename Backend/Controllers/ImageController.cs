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
            public ActionResult GetBannerPath()
            {
                  string imageDirectoryPath = "Uploads/Image/Banner"; // Đường dẫn tới thư mục chứa các file ảnh
                  string[] imageExtensions = { ".jpg", ".png", ".gif", "webp" }; // Các định dạng file ảnh bạn muốn lấy

                  // Lấy danh sách đường dẫn của các file ảnh trong thư mục tĩnh
                  string[] imagePaths = Directory.GetFiles(imageDirectoryPath)
                      .Where(file => imageExtensions.Contains(Path.GetExtension(file)))
                      .ToArray();
                  string prefix = string.Empty;
                  if (_env.IsDevelopment())
                  {
                        prefix = "https://localhost:7243/Contents/Image/Banner/";
                  }
                  else
                  {
                        prefix = "https://static.diydevblog.com/Image/Banner/";
                  }
                  List<string> imageNamesList = new List<string>();
                  foreach (string imagePath in imagePaths)
                  {
                        string imageName = Path.GetFileName(imagePath);
                        imageNamesList.Add(prefix + imageName);
                  }

                  return Ok(imageNamesList);
            }
      }
}