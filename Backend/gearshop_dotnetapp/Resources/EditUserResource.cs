using gearshop_dotnetapp.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace gearshop_dotnetapp.Resources
{
    public class EditUserResource
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
