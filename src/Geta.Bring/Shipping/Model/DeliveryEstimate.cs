using System;

namespace Geta.Bring.Shipping.Model
{
    /// <summary>
    /// Estimated delivery information.
    /// </summary>
    public class DeliveryEstimate : IEstimate
    {
        /// <summary>
        /// Initializes new instance of <see cref="DeliveryEstimate"/>
        /// </summary>
        /// <param name="product">Product for which delivery estimated.</param>
        /// <param name="expectedDelivery">Expected delivery information.</param>
        public DeliveryEstimate(Product product, ExpectedDelivery expectedDelivery)
        {
            ExpectedDelivery = expectedDelivery ?? throw new ArgumentNullException(nameof(expectedDelivery));
            Product = product ?? throw new ArgumentNullException(nameof(product));
        }

        /// <summary>
        /// Product for which delivery estimated <see cref="Product"/>.
        /// </summary>
        public Product Product { get; }

        /// <summary>
        /// Expected delivery information <see cref="ExpectedDelivery"/>.
        /// </summary>
        public ExpectedDelivery ExpectedDelivery { get; }
    }
}