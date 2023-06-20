using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace App.Controllers
{

      [Route("api/[controller]")]
      [ApiController]
      public class ImageController : ControllerBase
      {
            private readonly IHostEnvironment _env;
            private readonly IDistributedCache _cache;

            public ImageController(IHostEnvironment env, IDistributedCache cache)
            {
                  _env = env;
                  _cache = cache;
            }

            [HttpGet("/api/banner")]
            public async Task<ActionResult> GetBannerPath()
            {
                  List<string> imageNamesList = new List<string>();
                  var cachedData = await _cache.GetAsync("BannerPaths");
                  if (cachedData != null)
                  {
                        imageNamesList = JsonSerializer.Deserialize<List<string>>(cachedData);
                        return Ok(imageNamesList);
                  }
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
                  foreach (string imagePath in imagePaths)
                  {
                        string imageName = Path.GetFileName(imagePath);
                        imageNamesList.Add(prefix + imageName);
                  }
                  var cacheOptions = new DistributedCacheEntryOptions
                  {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(10) // Set the cache expiration time
                  };
                  var serializedData = JsonSerializer.Serialize(imageNamesList);
                  await _cache.SetStringAsync("BannerPaths", serializedData, cacheOptions);
                  return Ok(imageNamesList);
            }
      }
}