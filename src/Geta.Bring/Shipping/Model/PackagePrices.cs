using System;
using Newtonsoft.Json;

namespace Geta.Bring.Shipping.Model
{
    public class PackagePrices
    {
        public PackagePrices(PackagePrice listPrice)
        {
            ListPrice = listPrice ?? throw new ArgumentNullException(nameof(listPrice));
        }

        [JsonConstructor]
        public PackagePrices(PackagePrice listPrice, PackagePrice netPrice) : this(listPrice)
        {
            NetPrice = netPrice;
        }

        /// <summary>
        /// The public price for the product
        /// </summary>
        public PackagePrice ListPrice { get; }

        /// <summary>
        /// A specific agreement price for the product
        /// </summary>
        public PackagePrice NetPrice { get; }
    }
}