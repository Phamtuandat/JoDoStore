// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.ComponentModel.DataAnnotations;

namespace App.Areas.Identity.Models.AccountViewModels
{

      public class RegisterViewModel
      {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; } = string.Empty;


            [Required]
            [StringLength(100, MinimumLength = 2)]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm Password")]
            [Compare("Password")]
            public string ConfirmPassword { get; set; }


            [DataType(DataType.Text)]
            [Required]
            [StringLength(100, MinimumLength = 3)]
            public string UserName { get; set; }

            [Required]
            [StringLength(100, MinimumLength = 3)]
            [Display(Name = "First Name")]
            public string FirstName { get; set; }

            [Required]
            [StringLength(100, MinimumLength = 3)]
            [Display(Name = "Last Name")]
            public string LastName { get; set; }
      }
}