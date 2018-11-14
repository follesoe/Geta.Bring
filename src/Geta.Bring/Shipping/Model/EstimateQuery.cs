using System;
using System.Collections.Specialized;
using System.Linq;
using Geta.Bring.Shipping.Model.QueryParameters;

namespace Geta.Bring.Shipping.Model
{
    /// <summary>
    /// Query object for <see cref="ShippingClient.FindAsync{T}"/>.
    /// </summary>
    public class EstimateQuery
    {
        /// <summary>
        /// Initializes new instance of <see cref="EstimateQuery"/>.
        /// </summary>
        /// <param name="shipmentLeg">Shipment leg parameter <see cref="ShipmentLeg"/>.</param>
        /// <param name="packageSize">Package size parameter <see cref="PackageSize"/>.</param>
        /// <param name="additionalParameters">Additional parameters.</param>
        public EstimateQuery(
            ShipmentLeg shipmentLeg, 
            PackageSize packageSize,
            params IShippingQueryParameter[] additionalParameters)
        {
            if (shipmentLeg == null) throw new ArgumentNullException(nameof(shipmentLeg));
            if (packageSize == null) throw new ArgumentNullException(nameof(packageSize));

            var parameters = additionalParameters.ToList();
            parameters.ForEach(x =>
            {
                if (x == null)
                    throw new ArgumentException("additionalParameters contains null item", nameof(additionalParameters));
            });


            Items = new NameValueCollection
            {
                shipmentLeg.Items,
                packageSize.Items
            };

            parameters
                .ForEach(x => Items.Add(x.Items));
        }

        public NameValueCollection Items { get; }
    }
}