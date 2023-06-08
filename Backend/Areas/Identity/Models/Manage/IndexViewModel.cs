// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
using Microsoft.AspNetCore.Identity;


namespace App.Areas.Identity.Models.ManageViewModel
{
      public class IndexViewModel
      {
            public EditExtraProfileModel profile { get; set; }
            public bool HasPassword { get; set; }

            public IList<UserLoginInfo> Logins { get; set; }

            public string PhoneNumber { get; set; }

            public bool TwoFactor { get; set; }

            public bool BrowserRemembered { get; set; }

            public string AuthenticatorKey { get; set; }
      }
}
