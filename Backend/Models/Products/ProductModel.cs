namespace Backend.Models.Products
{
    public class ProductModel
    {
        public ProductModel()
        {
            this.Categories = new HashSet<Category>();
            this.Brands = new HashSet<BrandModel>();
        }
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public virtual ICollection<BrandModel> Brands { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public string Descriptions { get; set; } = String.Empty;
        public int Price { get; set; }
        public int PriceSale { get; set; }
        public DateTime CreatedTimestamp { get; set; }
        public string SmallImageLink { get; set; } = String.Empty;
        public string Thumbnail { get; set; } = string.Empty;
    }
}