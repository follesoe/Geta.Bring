using Geta.Bring.Shipping.Model;

namespace Geta.Bring.Shipping.Extensions
{
    internal static class ProductResponseExtensions
    {
        public static PackagePrice GetPackagePrices(this PackagePrices prices)
        {
            return prices.NetPrice ?? prices.ListPrice;
        }
    }
}