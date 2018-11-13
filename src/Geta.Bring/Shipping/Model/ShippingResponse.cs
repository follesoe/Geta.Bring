using System.Collections.Generic;
using System.Linq;
using Geta.Bring.Shipping.Infrastructure;
using Geta.Bring.Shipping.Model.Errors;
using Newtonsoft.Json;

namespace Geta.Bring.Shipping.Model
{
    internal class ShippingResponse
    {
        public ShippingResponse(IEnumerable<ConsignmentResponse> consignments, IEnumerable<string> traceMessages, IEnumerable<FieldError> fieldErrors)
        {
            Consignments = consignments ?? Enumerable.Empty<ConsignmentResponse>();
            TraceMessages = traceMessages ?? Enumerable.Empty<string>();
            FieldErrors = fieldErrors ?? Enumerable.Empty<FieldError>();
        }

        [JsonConverter(typeof(ObjectToArrayConverter<ConsignmentResponse>))]
        public IEnumerable<ConsignmentResponse> Consignments { get; }

        [JsonConverter(typeof(ObjectToArrayConverter<string>))]
        public IEnumerable<string> TraceMessages { get; }

        [JsonConverter(typeof(ObjectToArrayConverter<FieldError>))]
        public IEnumerable<FieldError> FieldErrors { get; }
    }
}