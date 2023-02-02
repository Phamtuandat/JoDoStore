using Backend.Services.Communication;
using gearshop_dotnetapp.Resources;

namespace gearshop_dotnetapp.Services.Communications
{
    public class BrandRes : BaseResponse
    {
        public BrandResource BrandResource { get; protected set; }
        public BrandRes(bool success, string message, BrandResource brandResource) : base(success, message)
        {
            BrandResource = brandResource;
        }
        public BrandRes(string message) : this(false, message, null)
        {

        }
        public BrandRes(bool success) :this(success, string.Empty, null)
        {

        }
        public BrandRes(BrandResource brandResource) : this (true, string.Empty, brandResource)
        {
        }
    }
}
