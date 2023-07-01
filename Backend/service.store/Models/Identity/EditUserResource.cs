namespace App.Models.Identity
{
      public class EditUserResource
      {
            public string FirstName { get; set; } = string.Empty;

            public string LastName { get; set; } = string.Empty;

            public DateTime Birthday { get; set; }

            public string Gender { get; set; } = string.Empty;
            public string PhoneNumber { get; set; } = string.Empty;
      }
}