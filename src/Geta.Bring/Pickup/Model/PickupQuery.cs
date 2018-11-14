using System;
using System.Collections.Specialized;
using System.Linq;

namespace Geta.Bring.Pickup.Model
{
    public class PickupQuery
    {
        /// <summary>
        /// Country code, ISO 2 letter, allowed values: NO, DK, SE, FI
        /// </summary>
        public readonly string CountryCode;

        /// <summary>
        /// A set of query parameters for the Pickup Point API
        /// </summary>
        /// <param name="countryCode">Country code, ISO 2 letter, allowed values: NO, DK, SE, FI</param>
        /// <param name="additionalParameters">Additional parameters</param>
        public PickupQuery(string countryCode, params IPickupQueryParameter[] additionalParameters)
        {
            if (string.IsNullOrWhiteSpace(countryCode))
                throw new ArgumentException("countryCode must be set");

            CountryCode = countryCode;

            var parameters = additionalParameters.ToList();
            parameters.ForEach(x =>
            {
                if (x == null)
                    throw new ArgumentException("additionalParameters contains null item", nameof(additionalParameters));
            });

            Items = new NameValueCollection();

            parameters
                .ForEach(x => Items.Add(x.Items));
        }

        public NameValueCollection Items { get; }
    }
}