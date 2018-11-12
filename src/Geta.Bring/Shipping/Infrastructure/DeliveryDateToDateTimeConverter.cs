using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Geta.Bring.Shipping.Infrastructure
{
    internal class DeliveryDateToDateTimeConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var token = JToken.Load(reader);

            if (token.Type == JTokenType.Object)
            {
                var year = token.Value<int?>("year") ?? 0;
                var month = token.Value<int?>("month") ?? 0;
                var day = token.Value<int?>("day") ?? 0;
                var hour = token.Value<int?>("hour") ?? 0;
                var minute = token.Value<int?>("minute") ?? 0;

                return new DateTime(year, month, day, hour, minute, 0);
            }

            throw new JsonSerializationException("Unexpected token type: " + token.Type);
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DateTime) || objectType == typeof(DateTime?);
        }
    }
}