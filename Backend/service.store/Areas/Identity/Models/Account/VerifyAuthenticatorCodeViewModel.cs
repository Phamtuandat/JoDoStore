// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.ComponentModel.DataAnnotations;
namespace App.Areas.Identity.Models.AccountViewModels
{

      public class VerifyAuthenticatorCodeViewModel
      {
            [Required]
            public string Code { get; set; } = string.Empty;

            public string ReturnUrl { get; set; } = string.Empty;

            [Display(Name = "Remember this Browser?")]
            public bool RememberBrowser { get; set; }

            [Display(Name = "Remember Me?")]
            public bool RememberMe { get; set; }
      }
}