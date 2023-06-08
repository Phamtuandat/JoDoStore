using System.Net;

namespace App.Exceptions
{
      public class ProductNotFoundException : BaseException
      {
            public ProductNotFoundException(string message) : base(message, HttpStatusCode.NotFound)
            {
            }
            public ProductNotFoundException() : base("Not Found!", HttpStatusCode.NotFound)
            {
            }
      }
}
