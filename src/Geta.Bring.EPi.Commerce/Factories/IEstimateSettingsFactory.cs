using Geta.Bring.EPi.Commerce.Model;
using Mediachase.Commerce.Orders.Dto;

namespace Geta.Bring.EPi.Commerce.Factories
{
    public interface IEstimateSettingsFactory
    {
        IEstimateSettings CreateFrom(ShippingMethodDto shippingMethod);
    }
}