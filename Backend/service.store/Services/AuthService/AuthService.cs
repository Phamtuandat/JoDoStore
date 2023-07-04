using System.Net.Http.Headers;
using System.Text.Json;
using App.Models;
using Newtonsoft.Json;

namespace App.Services;
public class AuthService : IAuthService
{
      private readonly IWebHostEnvironment _env;
      public AuthService(IWebHostEnvironment env)
      {
            _env = env;
      }
      public async Task<UserInfoResponse>? GetUserInfoAsync(string token)
      {
            string? userInfoEndpoint;
            if (_env.IsDevelopment())
            {
                  userInfoEndpoint = "https://localhost:5001/connect/userinfo";
            }
            else
            {
                  userInfoEndpoint = Environment.GetEnvironmentVariable("USERPROFILE_ENDPOINT");
            }
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var content = await client.GetStringAsync(userInfoEndpoint);
            var userinfo = JsonConvert.DeserializeObject<UserInfoResponse>(content);
            return userinfo;
      }
}