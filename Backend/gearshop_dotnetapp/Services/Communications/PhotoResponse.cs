using Backend.Services.Communication;
using gearshop_dotnetapp.Models.ProductModel;

namespace gearshop_dotnetapp.Services.Communications
{
    public class PhotoResponse : BaseResponse
    {
        public Photo Thumbnail { get; protected set; }
        public PhotoResponse(bool success, string message, Photo thumbnail) : base(success, message)
        {
            Thumbnail = thumbnail;
        }
        public PhotoResponse(string message) : this(false, message, null) { }
        public PhotoResponse(Photo thumbnail) : this (true, string.Empty, thumbnail) { }
        public PhotoResponse(Boolean success) : this(success, string.Empty, null) { }

    }
}
