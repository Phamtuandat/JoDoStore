﻿using Microsoft.AspNetCore.Mvc;

namespace App.Dtos
{
      public class EditUserDto
      {
            [FromBody]
            public string FirstName { get; set; } = string.Empty;

            [FromBody]
            public string LastName { get; set; } = string.Empty;

            [FromBody]
            public DateTime Birthday { get; set; }

            [FromBody]
            public string Gender { get; set; } = string.Empty;
            [FromBody]
            public string PhoneNumber { get; set; } = string.Empty;
      }
}
