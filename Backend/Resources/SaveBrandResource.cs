using System.ComponentModel.DataAnnotations;

namespace Backend
{
    public class SaveAuthorResourceModel
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}