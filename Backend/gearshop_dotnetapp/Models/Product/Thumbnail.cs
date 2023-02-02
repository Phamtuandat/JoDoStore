namespace gearshop_dotnetapp.Models.Product
{
    public class Photo
    {
        public int Id { get; set; } 
        public string PublicId { get; set; } = string.Empty; 
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public ImageCollections? ImageCollections { get; set; }
        public int? ProductId { get; set; } 
        public DateTime Created { get; set; }
    }
}
