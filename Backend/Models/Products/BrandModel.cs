using System.ComponentModel.DataAnnotations;

namespace Backend.Models.Products
{
    public class BrandModel
    {
        public BrandModel()
        {
            this.Books = new HashSet<ProductModel>();
        }
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public ICollection<ProductModel> Books { get; set; }
    }
}