// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.ComponentModel.DataAnnotations;
namespace App.Areas.Identity.Models.AccountViewModels
{

      public class ResetPasswordViewModel
      {
            [Required]
            [EmailAddress]
            public string Email { get; set; } = string.Empty;


            [Required]
            [StringLength(100, MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "New Password")]
            public string Password { get; set; } = string.Empty;

            [DataType(DataType.Password)]
            [Display(Name = "Confirm Password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; } = string.Empty;

            public string Code { get; set; } = string.Empty;

      }
}
