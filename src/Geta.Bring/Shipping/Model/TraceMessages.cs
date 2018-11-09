using System.Collections.Generic;
using System.Linq;
using Geta.Bring.Shipping.Infrastructure;
using Newtonsoft.Json;

namespace Geta.Bring.Shipping.Model
{
    public class TraceMessages
    {
        public TraceMessages(IEnumerable<string> message)
        {
            Message = message ?? Enumerable.Empty<string>();
        }

        [JsonConverter(typeof(ObjectToArrayConverter<string>))]
        public IEnumerable<string> Message { get; }
    }
}