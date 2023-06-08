using App.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace App.Areas.Identity.Models.UserViewModels
{
      public class AddUserRoleModel
      {
            public User user { get; set; }

            public string[] RoleNames { get; set; }

            public List<IdentityRoleClaim<string>> claimsInRole { get; set; }
            public List<IdentityUserClaim<string>> claimsInUserClaim { get; set; }

      }
}