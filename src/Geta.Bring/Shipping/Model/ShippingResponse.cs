using System.Collections.Generic;
using System.Linq;
using Geta.Bring.Shipping.Infrastructure;
using Newtonsoft.Json;

namespace Geta.Bring.Shipping.Model
{
    internal class ShippingResponse
    {
        public ShippingResponse(IEnumerable<ProductResponse> product, TraceMessages traceMessages, IEnumerable<FieldError> fieldErrors)
        {
            Product = product ?? Enumerable.Empty<ProductResponse>();
            TraceMessages = traceMessages ?? new TraceMessages(Enumerable.Empty<string>());
            FieldErrors = fieldErrors ?? Enumerable.Empty<FieldError>();
        }

        [JsonConverter(typeof(ObjectToArrayConverter<ProductResponse>))]
        public IEnumerable<ProductResponse> Product { get; private set; }

        public TraceMessages TraceMessages { get; private set; }

        [JsonConverter(typeof(ObjectToArrayConverter<FieldError>))]
        public IEnumerable<FieldError> FieldErrors { get; private set; }
    }
}