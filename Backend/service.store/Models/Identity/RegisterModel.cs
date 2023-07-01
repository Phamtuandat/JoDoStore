using System.ComponentModel.DataAnnotations;

namespace App.Models
{
      public class RegisterModel
      {
            [Required]
            public string Email { get; set; }


            [Required]
            [StringLength(100, MinimumLength = 2)]
            public string Password { get; set; }

            [Compare("Password")]
            public string ConfirmPassword { get; set; }


            [Required]
            [StringLength(100, MinimumLength = 3)]
            public string UserName { get; set; }

            [Required]
            [StringLength(100, MinimumLength = 3)]
            public string FirstName { get; set; }

            [Required]
            [StringLength(100, MinimumLength = 3)]
            public string LastName { get; set; }

      }
}