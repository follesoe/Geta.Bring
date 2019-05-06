using System.Net;

namespace Geta.Bring.Shipping.Model.Errors
{
    public class ResponseError : Error
    {
        public ResponseError(HttpStatusCode statusCode) : base($"RESPONSE_{statusCode}")
        {
            StatusCode = (int) statusCode;
        }

        public int StatusCode { get; set; }
    }
}