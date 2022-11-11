using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models.Products
{
    [Table("Brands")]
    public class BrandModel
    {
        public BrandModel()
        {
            this.Products = new HashSet<ProductModel>();
        }
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public ICollection<ProductModel> Products { get; set; }
    }
}