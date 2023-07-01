
using IdentityModel.Client;

namespace RealtimeApp.Services;
public interface ITokenService
{
      Task<TokenResponse> GetToken(string scope);
}