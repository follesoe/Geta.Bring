using System.Collections.Specialized;

namespace Geta.Bring.Shipping.Model.QueryParameters
{
    /// <summary>
    /// Query parameter to describe shipment source and destination.
    /// </summary>
    public class ShipmentLeg : IShippingQueryParameter
    {
        /// <summary>
        /// Initializes new instance of <see cref="ShipmentLeg"/> with postal codes from/to for Norway.
        /// </summary>
        /// <param name="postalCodeFrom">Norwegian source postal code.</param>
        /// <param name="postalCodeTo">Norwegian destination postal code.</param>
        public ShipmentLeg(string postalCodeFrom, string postalCodeTo)
        {
            Items = new NameValueCollection
            {
                {"frompostalcode", postalCodeFrom}, 
                {"topostalcode", postalCodeTo},
                {"fromcountry", "NO"}, 
                {"tocountry", "NO"}
            };
        }

        /// <summary>
        /// Initializes new instance of <see cref="ShipmentLeg"/> with postal codes from/to and country codes from/to.
        /// </summary>
        /// <param name="postalCodeFrom">Source postal code.</param>
        /// <param name="postalCodeTo">Destination postal code.</param>
        /// <param name="countryCodeFrom">Source country code in ISO 3 format.</param>
        /// <param name="countryCodeTo">Source country code in ISO 3 format.</param>
        public ShipmentLeg(
            string postalCodeFrom, 
            string postalCodeTo,
            string countryCodeFrom,
            string countryCodeTo)
        {
            Items = new NameValueCollection
            {
                {"frompostalcode", postalCodeFrom}, 
                {"topostalcode", postalCodeTo},
                {"fromcountry", countryCodeFrom}, 
                {"tocountry", countryCodeTo}
            }; 
        }

        public NameValueCollection Items { get; }
    }
}