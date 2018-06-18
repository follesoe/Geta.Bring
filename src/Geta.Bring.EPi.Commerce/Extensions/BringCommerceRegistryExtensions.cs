using Geta.Bring.EPi.Commerce.Factories;
using Mediachase.Commerce.Orders;
using StructureMap;

namespace Geta.Bring.EPi.Commerce.Extensions
{
    public static class BringCommerceRegistryExtensions
    {
        public static T RegisterBringContainers<T>(this T registry) where T : Registry
        {
            registry.For<IShippingPlugin>().Transient().Use<BringShippingGateway>();
            registry.For<IEstimateQueryFactory>().Transient().Use<EstimateQueryFactory>();
            return registry;
        }
    }
}
