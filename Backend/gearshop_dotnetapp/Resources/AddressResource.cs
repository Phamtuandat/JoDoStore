using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gearshop_dotnetapp.Resources
{
    public class AddressResource
    {
        public int Id { get; set; }
        public string? District { get; set; }
        public string? Province { set; get; }
        public string Ward { get; set; } = string.Empty;
        public string? Address { get; set; }
        public bool IsDefault { get; set; } = true;
        public string Name { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
