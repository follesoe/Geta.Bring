using System;
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
            CargoAgreementPrices cargoAgreementPrices) : 
                this(currencyCode, priceWithoutAdditionalServices, priceWithAdditionalServices)
        {
            CargoAgreementPrices = cargoAgreementPrices;
        }

        [Obsolete("Use CurrencyCode", true)]
        public string CurrencyIdentificationCode;

        [Obsolete("Use PriceWithoutAdditionalServices", true)]
        public Price PackagePriceWithoutAdditionalServices;

        [Obsolete("Use PriceWithAdditionalServices", true)]
        public Price PackagePriceWithAdditionalServices;

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
        public CargoAgreementPrices CargoAgreementPrices { get; }
    }
}