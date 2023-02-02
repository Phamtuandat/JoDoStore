using System.ComponentModel.DataAnnotations;

namespace gearshop_dotnetapp
{
    public class SaveBrandResource
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}