using System.ComponentModel.DataAnnotations;

namespace gearshop_dotnetapp.Models.ProductModel
{
    public class Category
    {
        public Category()
        {
            this.Products = new HashSet<Product>();
        }
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public virtual ICollection<Product> Products { get; set;} = new List<Product>();
    }
}
