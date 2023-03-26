using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace gearshop_dotnetapp.Resources
{
    public class SaveAddressResource
    {
        [FromBody]
        public string? District { get; set; }
        [FromBody]
        [MaxLength(50)]
        public string? Province { set; get; }
        [FromBody]
        [MaxLength(100)]
        public string? Address { get; set; }
        [FromBody]
        public string Name { get; set; } = string.Empty;
        [FromBody]
        public string PhoneNumber { get; set; } = string.Empty;
        public string Ward { get; set; }
    }
}
