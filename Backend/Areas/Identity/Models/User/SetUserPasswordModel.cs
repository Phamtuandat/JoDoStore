using System.ComponentModel.DataAnnotations;



namespace App.Areas.Identity.Models.UserViewModels
{
      public class SetUserPasswordModel
      {
            [Required]
            [StringLength(100, MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "New Password")]
            public string NewPassword { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm Password")]
            [Compare("NewPassword")]
            public string ConfirmPassword { get; set; }


      }
}