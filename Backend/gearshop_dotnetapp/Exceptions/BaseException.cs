
using System.Net;

namespace gearshop_dotnetapp.Exceptions
{
    public class BaseException : Exception
    {
        public HttpStatusCode StatusCode { get; }

        public BaseException(string message) : this(message, HttpStatusCode.InternalServerError)
        {
        }

        public BaseException(string message, Exception innerException) : this(message, innerException, HttpStatusCode.InternalServerError)
        {
        }

        public BaseException(string message, HttpStatusCode statusCode) : base(message)
        {
            StatusCode = statusCode;
        }

        public BaseException(string message, Exception innerException, HttpStatusCode statusCode) : base(message, innerException)
        {
            StatusCode = statusCode;
        }
    }
}
