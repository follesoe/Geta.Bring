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
            string currencyIdentificationCode,
            Price packagePriceWithoutAdditionalServices, 
            Price packagePriceWithAdditionalServices)
        {
            PackagePriceWithAdditionalServices = packagePriceWithAdditionalServices ?? throw new ArgumentNullException(nameof(packagePriceWithAdditionalServices));
            PackagePriceWithoutAdditionalServices = packagePriceWithoutAdditionalServices ?? throw new ArgumentNullException(nameof(packagePriceWithoutAdditionalServices));
            CurrencyIdentificationCode = currencyIdentificationCode ?? throw new ArgumentNullException(nameof(currencyIdentificationCode));
        }

        [JsonConstructor]
        public PackagePrice(
            string currencyIdentificationCode,
            Price packagePriceWithoutAdditionalServices, 
            Price packagePriceWithAdditionalServices,
            CargoAgreementPrices cargoAgreementPrices) : 
                this(currencyIdentificationCode, packagePriceWithoutAdditionalServices, packagePriceWithAdditionalServices)
        {
            CargoAgreementPrices = cargoAgreementPrices;
        }

        /// <summary>
        /// Currency code.
        /// </summary>
        [JsonProperty("currencyCode")]
        public string CurrencyIdentificationCode { get; private set; }

        /// <summary>
        /// Price without additional services.
        /// </summary>
        public Price PackagePriceWithoutAdditionalServices { get; }

        /// <summary>
        /// Price with additional services.
        /// </summary>
        public Price PackagePriceWithAdditionalServices { get; }

        /// <summary>
        /// Special cargo agreement prices.
        /// </summary>
        public CargoAgreementPrices CargoAgreementPrices { get; }
    }
}