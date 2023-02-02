using gearshop_dotnetapp.Models.Product;

namespace gearshop_dotnetapp.Resources
{
    public class PhotoResource
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public ImageCollectionResource? ImageCollections { get; set; }
    }
}
