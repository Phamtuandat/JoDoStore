using Backend.Models.Products;

namespace Backend.Resources
{
    public class BrandResource
    {
        public int? Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;


    }
}
