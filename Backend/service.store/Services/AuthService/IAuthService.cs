using App.Models;

namespace App.Services;

public interface IAuthService{
      Task<UserInfoResponse>? GetUserInfoAsync(string token);
}