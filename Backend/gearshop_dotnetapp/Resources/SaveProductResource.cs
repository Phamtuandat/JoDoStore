
using gearshop_dotnetapp.Models.Product;
using gearshop_dotnetapp.Resources;
using System.ComponentModel.DataAnnotations;

namespace gearshop_dotnetapp
{
    public class SaveProductResource
    {
        [Required]
        [MaxLength(225)]
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ICollection<IFormFile>? Thumbnails { get; set; } 
        [Required]
        public SaveCategoryResource Category { get; set; } = new();
        [Required]
        public SaveBrandResource Brand { get; set; } = new();
        public string[]? Tags { get; set; } 
        [Required]
        public int Price { get; set; }
        public int SalePrice { get; set; }
    }
}