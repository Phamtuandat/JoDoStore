using Backend.Models.Identity;

namespace gearshop_dotnetapp.Resources
{
    public class UserResource
    {
        public string Id { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public virtual AddressBook? Address { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public DateOnly Birthday { get; set; }
        public string? Gender { get; set; }
        public CartResource? Cart { get; set; }
    }

}
