namespace gearshop_dotnetapp
{
    public class SavPhotoResource
    {
        public string Title { get; set; } = string.Empty;
        public string Collections { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public IFormFile? FormFile { get; set; }
        public int? ProductId { get; set; }
    }
}