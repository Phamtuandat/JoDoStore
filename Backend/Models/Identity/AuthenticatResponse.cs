using Backend.Resources;
using Backend.Services.Communication;

namespace Backend.Models.Identity
{
    public class AuthenticatResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public AuthenticatResponse(UserResource resource, string token, string refreshToken)
        {
            Id= resource.Id;
            FirstName= resource.FirstName;
            LastName= resource.LastName;
            Email= resource.Email;
            Token = token;
            RefreshToken = refreshToken;
        }
    }
}
