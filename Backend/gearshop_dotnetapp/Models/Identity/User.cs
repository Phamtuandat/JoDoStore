using Backend.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace gearshop_dotnetapp.Models.Identity
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DateTime { get; set; } = DateTime.UtcNow;
        public Adress? Adress { get; set; }
        public override string? UserName { get => base.UserName; set => base.UserName = value; }
#pragma warning disable CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).
        public override string Email { get; set; } = string.Empty;
#pragma warning restore CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).
    }

}
