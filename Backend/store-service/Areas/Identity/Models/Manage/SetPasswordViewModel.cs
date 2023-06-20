// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.ComponentModel.DataAnnotations;
namespace App.Areas.Identity.Models.ManageViewModel
{
      public class SetPasswordViewModel
      {
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
