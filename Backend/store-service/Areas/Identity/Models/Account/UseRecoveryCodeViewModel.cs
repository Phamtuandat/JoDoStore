// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.ComponentModel.DataAnnotations;
namespace App.Areas.Identity.Models.AccountViewModels
{

      public class UseRecoveryCodeViewModel
      {

            [Required]
            [Display(Name = "Restore Code")]
            public string Code { get; set; } = string.Empty;

            public string ReturnUrl { get; set; } = string.Empty;
      }
}

