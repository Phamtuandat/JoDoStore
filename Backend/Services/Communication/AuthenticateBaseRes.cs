using Backend.Models.Identity;

namespace Backend.Services.Communication
{
    public class AuthenticateBaseRes : BaseResponse
    {
        public User User { get; private set; }
        public string? Token { get; private set; } 
        public string? RefreshToken { get; private set; }
        private AuthenticateBaseRes(bool success, string message, User user, string token, string refreshToken) : base(success, message)
        {

            User = user;
            Token = token;
            RefreshToken = refreshToken;
        }
        public AuthenticateBaseRes(User user, string token, string refreshToken) : this(true, string.Empty, user, token, refreshToken)
        {
        }
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        public AuthenticateBaseRes(string message) : this(false, message, null, null, null) { }
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
    }
}
