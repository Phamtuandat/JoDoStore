using gearshop_dotnetapp.Models.Product;

namespace gearshop_dotnetapp.Resources
{
    public class ProductResource
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public CategoryResource Category { get; set; } = new();
        public BrandResource Brand { get; set; } = new();
        public ICollection<PhotoResource> Thumbnails { get; set; } = new List<PhotoResource>();
        public int Price { get; set; }
        public int SalePrice { get; set; }
        public ICollection<Tag> Tags { get; set; } = new List<Tag>();

        public DateTime CreateAt = DateTime.UtcNow;
    }
}
