namespace Backend.Resources
{
    public class SaveProductResource
    {
        public string Name { get; set; } = string.Empty;
        public int Brand { get; set; }
        public List<int> CategoryList { get; set; } = new List<int>();
        public string Descriptions { get; set; } = string.Empty;
        public int Price { get; set; }
        public int PriceSale { get; set; }
        public List<IFormFile> Media { get; set; }
        public string? SmallImageLink { get; set; }
        public string? Tags { get; set; }
    }


}
