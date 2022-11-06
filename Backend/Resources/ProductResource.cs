using Backend.Models.Products;

namespace Backend.Resources
{
    public class ProductResource
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<CategoryResource> Categories { get; set; } = new List<CategoryResource>();
        public ICollection<BrandResource> Brands { get; set; } = new List<BrandResource>();
        public int Price { get; set; }
        public int PriceSale { get; set; }
        public string Description { get; set; } = String.Empty;

        public string SmallImageLink { get; set; } = String.Empty;
        public string Thumbnail { get; set; } = string.Empty;

        
    }
}
