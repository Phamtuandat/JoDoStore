using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace App.Areas.Identity.Models.RoleViewModels
{
      public class EditRoleModel
      {
            [Display(Name = "Role Name")]
            [Required]
            [StringLength(256, MinimumLength = 3)]
            public string Name { get; set; }
            public List<IdentityRoleClaim<string>> Claims { get; set; }

            public IdentityRole Role { get; set; }




      }
}
