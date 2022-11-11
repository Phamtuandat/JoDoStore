using Backend.Models.Products;

namespace Backend.Resources
{
    public class ProductResource
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<CategoryResource> Categories { get; set; } = new List<CategoryResource>();
        public BrandResource Brand { get; set; } 
        public int Price { get; set; }
        public int PriceSale { get; set; }
        public string Description { get; set; } = string.Empty;

        public string SmallImageLink { get; set; } = string.Empty;

        public ICollection<MediaResource> MediaResources { get; set; } = new List<MediaResource>();
        
    }
}
