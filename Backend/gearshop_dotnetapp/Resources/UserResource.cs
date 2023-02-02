using Backend.Models.Identity;

namespace gearshop_dotnetapp.Resources
{
    public class UserResource
    {
        public string Id { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public Adress Adress { get; set; } = new();
    }

}
