using System.ComponentModel.DataAnnotations;

namespace gearshop_dotnetapp.Resources
{
    public class SaveAddressResource
    {
        public string? City { get; set; }

        [MaxLength(50)]
        public string? State { set; get; }

        [MaxLength(100)]
        public string? StreetAddress { get; set; }
    }
}
