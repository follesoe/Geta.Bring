using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Geta.Bring.Booking.Model;

namespace Geta.Bring.Booking.Infrastructure
{
    public class NatureOfTransactionConverter : JsonConverter
    {
        private const string SaleOfGoods = "SALE_OF_GOODS";
        private const string ReturnedGoods = "RETURNED_GOODS";
        private const string Gift = "GIFT";
        private const string CommercialSample = "COMMERCIAL_SAMPLE";
        private const string Documents = "DOCUMENTS";
        private const string Other = "OTHER";

        public NatureOfTransactionConverter()
        {
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var natureOfTransaction = (NatureOfTransaction)value;

            switch(natureOfTransaction) {
                case NatureOfTransaction.SaleOfGoods:
                    writer.WriteValue(SaleOfGoods);
                    break;
                case NatureOfTransaction.ReturnedGoods:
                    writer.WriteValue(ReturnedGoods);
                    break;
                case NatureOfTransaction.Gift:
                    writer.WriteValue(Gift);
                    break;
                case NatureOfTransaction.CommercialSample:
                    writer.WriteValue(CommercialSample);
                    break;
                case NatureOfTransaction.Documents:
                    writer.WriteValue(Documents);
                    break;
                case NatureOfTransaction.Other:
                    writer.WriteValue(Other);
                    break;
                default:
                    throw new ArgumentException($"Unhandlet enum value: {natureOfTransaction}");
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var enumString = (string)reader.Value;
            switch (enumString)
            {
                case SaleOfGoods:
                    return NatureOfTransaction.SaleOfGoods;
                case ReturnedGoods:
                    return NatureOfTransaction.ReturnedGoods;
                case Gift:
                    return NatureOfTransaction.Gift;
                case CommercialSample:
                    return NatureOfTransaction.CommercialSample;
                case Documents:
                    return NatureOfTransaction.Documents;
                case Other:
                    return NatureOfTransaction.Other;
                default:
                    throw new ArgumentException($"Unable to convert {enumString} to {nameof(NatureOfTransaction)}");
            }
        }

        public override bool CanConvert(Type objectType) => objectType == typeof(string);
    }
}