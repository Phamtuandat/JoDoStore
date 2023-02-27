using System.Net;

namespace gearshop_dotnetapp.Exceptions
{
    public class ProductNotFoundException : BaseException
    {
        public ProductNotFoundException(string message) : base(message, HttpStatusCode.NotFound)
        {
        }
    }
}
