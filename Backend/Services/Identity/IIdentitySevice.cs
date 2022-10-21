using Backend.Models.Identity;
using Backend.Services.Communication;

namespace Backend.Services.Identity
{
    public interface IIdentitySevice
    {
        Task<AuthenticateBaseRes>? RegisterAsync(RegisterRequest model);
        Task<AuthenticateBaseRes>? AuthenticateAsync(AuthenticateRequest model);
        IEnumerable<User> GetAll();
        User GetById(int id);

        Task<AuthenticateBaseRes> GetUserByToken(string email);
    }
}
