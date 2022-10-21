using System.ComponentModel.DataAnnotations;

namespace Backend.Resources
{
    public class SaveCategoryResource
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
    }
}
