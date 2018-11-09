namespace Geta.Bring.Shipping.Model
{
    /// <summary>
    /// Price.
    /// </summary>
    public class Price
    {
        public Price(
            double amountWithoutVat, 
            double vat, 
            double amountWithVat)
        {
            AmountWithVAT = amountWithVat;
            VAT = vat;
            AmountWithoutVAT = amountWithoutVat;
        }

        /// <summary>
        /// Price without VAT in NOK.
        /// </summary>
        public double AmountWithoutVAT { get; }

        /// <summary>
        /// VAT in NOK.
        /// </summary>
        public double VAT { get; }

        /// <summary>
        /// Price with VAT in NOK.
        /// </summary>
        public double AmountWithVAT { get; }
    }
}