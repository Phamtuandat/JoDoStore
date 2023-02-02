using System.ComponentModel.DataAnnotations;

namespace gearshop_dotnetapp.Resources
{
    public class SaveUserResource
    {
        [Required]
        [MaxLength(255)]
        public string Email { get; set; } = string.Empty;
        [Required]
        [MaxLength(50)]
        public string Password { get; set; } = string.Empty;
    }
}
