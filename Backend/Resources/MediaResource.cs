using Backend.Models.Products;

namespace Backend
{
    public class MediaResource
    {
        public int Id { get; set; }
        public string ThumbnailPath { get; set; }

        public MediaResource(Media media )
        {
            Id = media.Id;
            ThumbnailPath = media.thumbnailPath;
        }

    }
}