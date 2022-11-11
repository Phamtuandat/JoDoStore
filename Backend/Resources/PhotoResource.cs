namespace Backend.Resources
{
    public class PhotoResource
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;
        public IFormFile ImageFile { get; set; } = null;
    }
}
