using Microsoft.AspNetCore.Mvc;

namespace App.Dtos
{
      public class ChangePwDto
      {
            [FromBody]
            public string CurrentPassword { get; set; } = string.Empty;
            [FromBody]
            public string NewPassword { get; set; } = string.Empty;
            [FromBody]
            public string ComfirmPassword { get; set; } = string.Empty;
      }
}
