using System;

namespace Geta.Bring.Shipping.Model
{
    public class AgreementPrice
    {
        public AgreementPrice(
            string agreementName, 
            int agreementNumber,
            string subAgreementName,
            int subAgreementNumber,
            double price)
        {
            AgreementName = agreementName ?? throw new ArgumentNullException(nameof(agreementName));
            AgreementNumber = agreementNumber;
            SubAgreementName = subAgreementName;
            SubAgreementNumber = subAgreementNumber;
            Price = price;
        }

        /// <summary>
        /// Name of main agreement
        /// </summary>
        public string AgreementName { get; }

        /// <summary>
        /// Main agreement identifier
        /// </summary>
        public int AgreementNumber { get; }

        /// <summary>
        /// Sub agreement name
        /// </summary>
        public string SubAgreementName { get; }

        /// <summary>
        /// Sub agreement identifier
        /// </summary>
        public int SubAgreementNumber { get; }

        /// <summary>
        /// Agreement price w/o VAT (default NOK)
        /// </summary>
        public double Price { get; }
    }
}