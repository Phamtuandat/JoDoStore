
namespace App.Dtos
{
      public class UserResource
      {
            public string Id { get; set; } = string.Empty;
            public string FirstName { get; set; } = string.Empty;
            public string LastName { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public string PhoneNumber { get; set; } = string.Empty;
            public DateOnly Birthday { get; set; }
            public string Gender { get; set; } = string.Empty;
            public bool EmailConfirmed { get; set; }
      }

}
