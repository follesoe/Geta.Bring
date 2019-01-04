using System;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Geta.Bring.Booking.Infrastructure
{
    public class MilisecondEpochConverter : DateTimeConverterBase
    {
        private static readonly DateTime _epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                return;
            }
            var miliseconds = Convert.ToInt64(((DateTime) value - _epoch).TotalMilliseconds);
            writer.WriteRawValue((miliseconds.ToString(CultureInfo.InvariantCulture)));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value == null)
            {
                return null;
            }
            return _epoch.AddMilliseconds((long)reader.Value);
        }
    }
}