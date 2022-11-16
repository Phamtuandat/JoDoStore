namespace Backend.Models.Products
{
    public class Media
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string ThumbnailPath { get; set; } = string.Empty;
        public virtual ProductModel Product { get; set; }

    }
}
