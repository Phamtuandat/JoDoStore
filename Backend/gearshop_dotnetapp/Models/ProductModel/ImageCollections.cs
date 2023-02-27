namespace gearshop_dotnetapp.Models.ProductModel
{
    public class ImageCollections
    {
        public ImageCollections()
        {
            this.Thumbnails = new HashSet<Photo>();
        }
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public virtual ICollection<Photo> Thumbnails { get; set; }
    }
}
