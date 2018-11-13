using System.Net;

namespace Geta.Bring.Shipping.Model.Errors
{
    public class ResponseError : Error
    {
        public ResponseError(HttpStatusCode statusCode)
        {
            StatusCode = (int) statusCode;
            Code = $"RESPONSE_{StatusCode}";
        }

        public int StatusCode { get; set; }
    }
}