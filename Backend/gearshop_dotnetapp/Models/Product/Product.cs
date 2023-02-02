using System.ComponentModel.DataAnnotations.Schema;

namespace gearshop_dotnetapp.Models.Product
{
    public class Product

    {
        public Product()
        {
            this.Thumbnails =  new HashSet<Photo>();
            this.Tags = new HashSet<Tag>();
        }
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public string NormalizedName { set; get; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        [ForeignKey("CaegoryId")]
        public Category? Category { get; set; } = new Category();
        public Brand? Brand { get; set; }
        public ICollection<Tag>? Tags { get; set; }
        public ICollection<Photo> Thumbnails { get; set; }

        public int Price { get; set; }
        public int SalePrice { get; set; }

        public DateTime CreateAt = DateTime.UtcNow;
    }
}
