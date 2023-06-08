using System.ComponentModel.DataAnnotations;

namespace App.Areas.Identity.Models.RoleViewModels
{
      public class CreateRoleModel
      {
            [Display(Name = "Role Name")]
            [Required]
            [StringLength(256)]
            public string Name { get; set; }


      }
}
