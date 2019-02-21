using System;
using System.Collections.Generic;
using System.Linq;
using Geta.Bring.Shipping.Infrastructure;
using Newtonsoft.Json;

namespace Geta.Bring.Shipping.Model
{
    /// <summary>
    /// Price information.
    /// </summary>
    public class PackagePrice
    {
        public PackagePrice(
            string currencyCode,
            Price priceWithoutAdditionalServices, 
            Price priceWithAdditionalServices)
        {
            PriceWithAdditionalServices = priceWithAdditionalServices ?? throw new ArgumentNullException(nameof(priceWithAdditionalServices));
            PriceWithoutAdditionalServices = priceWithoutAdditionalServices ?? throw new ArgumentNullException(nameof(priceWithoutAdditionalServices));
            CurrencyCode = currencyCode ?? throw new ArgumentNullException(nameof(currencyCode));
        }

        [JsonConstructor]
        public PackagePrice(
            string currencyCode,
            Price priceWithoutAdditionalServices, 
            Price priceWithAdditionalServices,
            IEnumerable<AgreementPrice> cargoAgreementPrices = null) : 
                this(currencyCode, priceWithoutAdditionalServices, priceWithAdditionalServices)
        {
            CargoAgreementPrices = cargoAgreementPrices ?? Enumerable.Empty<AgreementPrice>();
        }

        /// <summary>
        /// Currency code.
        /// </summary>
        public string CurrencyCode;

        /// <summary>
        /// Price without additional services.
        /// </summary>       
        public Price PriceWithoutAdditionalServices { get; }

        /// <summary>
        /// Price with additional services.
        /// </summary>
        public Price PriceWithAdditionalServices { get; }

        /// <summary>
        /// Special cargo agreement prices.
        /// </summary>
        [JsonConverter(typeof(ObjectToArrayConverter<AgreementPrice>))]
        public IEnumerable<AgreementPrice> CargoAgreementPrices { get; }
    }
}