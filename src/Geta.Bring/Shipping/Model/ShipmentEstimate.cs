using System;

namespace Geta.Bring.Shipping.Model
{
    /// <summary>
    /// Estimated shipment information.
    /// </summary>
    public class ShipmentEstimate : IEstimate
    {
        /// <summary>
        /// Initializes new instance of <see cref="ShipmentEstimate"/>.
        /// </summary>
        /// <param name="product">Product for which shipment estimated.</param>
        /// <param name="guiInformation">GUI information.</param>
        /// <param name="packagePrice">Price information.</param>
        /// <param name="expectedDelivery">Expected delivery information.</param>
        public ShipmentEstimate(
            Product product, 
            GuiInformation guiInformation, 
            PackagePrice packagePrice, 
            ExpectedDelivery expectedDelivery)
        {
            ExpectedDelivery = expectedDelivery ?? throw new ArgumentNullException(nameof(expectedDelivery));
            PackagePrice = packagePrice ?? throw new ArgumentNullException(nameof(packagePrice));
            GuiInformation = guiInformation ?? throw new ArgumentNullException(nameof(guiInformation));
            Product = product ?? throw new ArgumentNullException(nameof(product));
        }

        /// <summary>
        /// Product for which shipment estimated. 
        /// </summary>
        public Product Product { get; }

        /// <summary>
        /// GUI information <see cref="GuiInformation"/>.
        /// </summary>
        public GuiInformation GuiInformation { get; }

        /// <summary>
        /// Price information <see cref="PackagePrice"/>.
        /// </summary>
        public PackagePrice PackagePrice { get; }

        /// <summary>
        /// Expected delivery information <see cref="ExpectedDelivery"/>.
        /// </summary>
        public ExpectedDelivery ExpectedDelivery { get; }
    }
}