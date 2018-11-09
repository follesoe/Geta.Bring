using System;
using System.Collections.Generic;

namespace Geta.Bring.Booking.Model
{
    /// <summary>
    /// Consignment information.
    /// </summary>
    public class Consignment
    {
        /// <summary>
        /// Initializes new instance of <see cref="Consignment"/>.
        /// </summary>
        /// <param name="correlationId">Correlation ID.</param>
        /// <param name="shippingDateTime">Shipping date and time to Bring.</param>
        /// <param name="parties">Parties information. For more see: <see cref="Parties"/>.</param>
        /// <param name="product">Product information. For more see: <see cref="Product"/>.</param>
        /// <param name="packages">List of package information included in consignment. For more see <see cref="Package"/>.</param>
        public Consignment(
            string correlationId, 
            DateTime shippingDateTime, 
            Parties parties, 
            Product product, 
            IEnumerable<Package> packages)
        {
            Packages = packages ?? throw new ArgumentNullException(nameof(packages));
            Product = product ?? throw new ArgumentNullException(nameof(product));
            ShippingDateTime = shippingDateTime;
            Parties = parties ?? throw new ArgumentNullException(nameof(parties));
            CorrelationId = correlationId ?? throw new ArgumentNullException(nameof(correlationId));
        }

        /// <summary>
        /// Correlation ID.
        /// </summary>
        public string CorrelationId { get; }

        /// <summary>
        /// Shipping date and time to Bring.
        /// </summary>
        public DateTime ShippingDateTime { get; }

        /// <summary>
        /// Parties information. For more see: <see cref="Parties"/>.
        /// </summary>
        public Parties Parties { get; }

        /// <summary>
        /// Product information. For more see: <see cref="Product"/>.
        /// </summary>
        public Product Product { get; }

        /// <summary>
        /// List of package information included in consignment. For more see <see cref="Package"/>.
        /// </summary>
        public IEnumerable<Package> Packages { get; }
    }
}