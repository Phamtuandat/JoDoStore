using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace App.Areas.Identity.Models.RoleViewModels
{
      public class EditClaimModel
      {
            [Required]
            [StringLength(256, MinimumLength = 3)]
            public string ClaimType { get; set; }
            [Required]
            [StringLength(256, MinimumLength = 3)]
            public string ClaimValue { get; set; }
            public IdentityRole role { get; set; }

      }
}
