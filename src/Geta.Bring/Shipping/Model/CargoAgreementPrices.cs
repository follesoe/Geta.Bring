using System;

namespace Geta.Bring.Shipping.Model
{
    public class CargoAgreementPrices
    {
        public CargoAgreementPrices(AgreementPrice cargoAgreementPrice)
        {
            CargoAgreementPrice = cargoAgreementPrice ?? throw new ArgumentNullException(nameof(cargoAgreementPrice));
        }

        /// <summary>
        /// Cargo agreement
        /// </summary>
        public AgreementPrice CargoAgreementPrice { get; }
    }
}