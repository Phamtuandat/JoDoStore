using Backend.Services.Communication;
using gearshop_dotnetapp.Resources;

namespace gearshop_dotnetapp.Services.Communications
{
    public class ProductRes : BaseResponse
    {
        public ProductResource ProductResource { get; protected set; }
        public ProductRes(bool success, string message, ProductResource productResource) : base(success, message)
        {
            ProductResource = productResource;
        }
        public ProductRes(ProductResource product) : this(true, string.Empty, product)
        {

        }
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        public ProductRes(string message) : this(false, message, null)
        {

        }
        public ProductRes(bool success)  : this(success, string.Empty, null)
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        {

        }
    }
}
