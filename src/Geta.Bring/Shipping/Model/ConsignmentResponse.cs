using System.Collections.Generic;
using System.Linq;
using Geta.Bring.Shipping.Infrastructure;
using Newtonsoft.Json;

namespace Geta.Bring.Shipping.Model
{
    internal class ConsignmentResponse
    {
        public ConsignmentResponse(IEnumerable<ProductResponse> products)
        {
            Products = products ?? Enumerable.Empty<ProductResponse>();
        }

        [JsonConverter(typeof(ObjectToArrayConverter<ProductResponse>))]
        public IEnumerable<ProductResponse> Products { get; }

        // todo: Add support for other consignment types
    }
}