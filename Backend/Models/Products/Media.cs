namespace Backend.Models.Products
{
    public class Media
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string thumbnailPath { get; set; }
        public ProductModel Product { get; set; }

    }
}
