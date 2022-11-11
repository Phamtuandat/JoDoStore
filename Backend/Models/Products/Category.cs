using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models.Products
{

    public class Category
    {
        public Category()
        {
            this.Products = new HashSet<ProductModel>();
        }
        public int Id { get; set; }

        [Required]
        [MaxLength(225)]
        public string Name { get; set; } = string.Empty;

        public virtual ICollection<ProductModel> Products { get; set; }
        public string CategoryThumb { get; set; } =  string.Empty;
    }
}
