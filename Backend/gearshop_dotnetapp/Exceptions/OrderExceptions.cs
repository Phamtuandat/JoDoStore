using System.Net;

namespace gearshop_dotnetapp.Exceptions
{
    public class OrderNotFoundException : BaseException
    {
        public OrderNotFoundException(string message) : base(message, HttpStatusCode.NotFound )
        {
        }
    }
    public class OrderProcessingException : BaseException
    {
        public OrderProcessingException(string message) : base(message)
        {
        }
    }
    public class OrderValidationException : BaseException
    {
        public OrderValidationException(string message) : base(message, HttpStatusCode.InternalServerError) { }

    }
}
