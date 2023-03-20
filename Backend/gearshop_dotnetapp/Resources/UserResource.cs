using Backend.Models.Identity;
using gearshop_dotnetapp.Enums;
using gearshop_dotnetapp.Models.Identity;

namespace gearshop_dotnetapp.Resources
{
    public class UserResource
    {
        public string Id { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public virtual Address Address { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public DateOnly Birthday { get; set; }
        public string Gender { get; set; }
        
    }

}
