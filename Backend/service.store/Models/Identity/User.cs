using Microsoft.AspNetCore.Identity;

namespace App.Models.Identity
{
      public class User : IdentityUser
      {
            public string FirstName { get; set; } = string.Empty;
            public string LastName { get; set; } = string.Empty;
            public DateTime CreateAt { get; set; } = DateTime.UtcNow;
            public override string? UserName { get => base.UserName; set => base.UserName = value; }
            public DateOnly Birthday { get; set; }
      }

}
