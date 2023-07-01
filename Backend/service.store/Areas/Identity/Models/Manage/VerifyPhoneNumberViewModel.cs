// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.ComponentModel.DataAnnotations;
namespace App.Areas.Identity.Models.ManageViewModel
{
      public class VerifyPhoneNumberViewModel
      {
            public string Code { get; set; }

            [Required]
            [Phone]
            [Display(Name = "Phone Number")]
            public string PhoneNumber { get; set; }
      }
}
