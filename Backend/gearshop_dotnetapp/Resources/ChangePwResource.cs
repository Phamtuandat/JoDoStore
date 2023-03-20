using Microsoft.AspNetCore.Mvc;

namespace gearshop_dotnetapp.Resources
{
    public class ChangePwResource
    {
        [FromBody]
        public string CurrentPassword { get; set; } = string.Empty;
        [FromBody]
        public string NewPassword { get; set; } = string.Empty;
        [FromBody]
        public string ComfirmPassword { get; set; } = string.Empty;
    }
}
