// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.ComponentModel.DataAnnotations;

namespace App.Areas.Identity.Models.AccountViewModels
{
      public class LoginViewModel
      {
            [Required]
            [Display(Name = "Username or Email")]
            public string UserNameOrEmail { get; set; } = string.Empty;


            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Mật khẩu")]
            public string Password { get; set; } = string.Empty;

            [Display(Name = "Remember Me?")]
            public bool RememberMe { get; set; }
      }
}

