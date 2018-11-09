using System;

namespace Geta.Bring.Shipping.Model
{
    /// <summary>
    /// Estimated price information.
    /// </summary>
    public class PriceEstimate : IEstimate
    {
        /// <summary>
        /// Initializes new instance of <see cref="PriceEstimate"/>.
        /// </summary>
        /// <param name="product">Product for which price estimated.</param>
        /// <param name="prices">Price information.</param>
        public PriceEstimate(Product product, PackagePrices prices)
        {
            Prices = prices ?? throw new ArgumentNullException(nameof(prices));
            Product = product ?? throw new ArgumentNullException(nameof(product));
        }

        /// <summary>
        /// Product for which price estimated <see cref="Product"/>.
        /// </summary>
        public Product Product { get; }

        /// <summary>
        /// Price information <see cref="PackagePrices"/>.
        /// </summary>
        public PackagePrices Prices { get; }
    }
}