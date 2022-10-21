namespace Backend.Resources
{
    public class SaveBookResource
    {
        public string Name { get; set; }
        public List<int> Authors { get; set; }
        public List<int> CategoryList { get; set; }
        public string Descriptions { get; set; }
        public int Price { get; set; }
        public int PriceSale { get; set; }
    }
}
