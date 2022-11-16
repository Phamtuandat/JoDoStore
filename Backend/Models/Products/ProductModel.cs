using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models.Products
{
    [Table("Products")]

    public class ProductModel
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public ProductModel()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            this.Categories = new HashSet<Category>();
            this.Media = new HashSet<Media>();
        }
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public BrandModel Brand { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public string Descriptions { get; set; } = string.Empty;
        public int Price { get; set; }
        public int PriceSale { get; set; }
        public DateTime CreatedTimestamp { get; set; }
        public string SmallImageLink { get; set; } = string.Empty;
        public virtual ICollection<Media> Media { get; set; }

    }

}