using System.ComponentModel.DataAnnotations;

namespace App.Areas.Identity.Models.UserViewModels
{
      public class AddUserClaimModel
      {
            [Required]
            [StringLength(256, MinimumLength = 3)]
            public string ClaimType { get; set; }

            [Required]
            [StringLength(256, MinimumLength = 3)]
            public string ClaimValue { get; set; }

      }
}