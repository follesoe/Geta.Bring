using System;
using EPiServer.Commerce.Order;
using Geta.Bring.Shipping.Model;

namespace Geta.Bring.EPi.Commerce.Factories
{
    public interface IEstimateQueryFactory
    {
        EstimateQuery BuildEstimateQuery(IShipment shipment, Guid shippingMethodId);
    }
}