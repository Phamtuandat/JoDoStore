namespace Backend.Resources
{
    public class SaveProductResource
    {
        public string Name { get; set; } = string.Empty;
        public List<int> Brands { get; set; } = new List<int>();
        public List<int> CategoryList { get; set; } = new List<int>();
        public string Descriptions { get; set; } = string.Empty;
        public int Price { get; set; }
        public int PriceSale { get; set; }
    }
}
