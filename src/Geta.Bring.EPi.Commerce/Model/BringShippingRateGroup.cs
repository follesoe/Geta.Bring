using System;
using System.Collections.Generic;

namespace Geta.Bring.EPi.Commerce.Model
{
    public class BringShippingRateGroup
    {
        public BringShippingRateGroup(string name, IEnumerable<BringShippingRate> shippingRates)
        {
            ShippingRates = shippingRates ?? throw new ArgumentNullException(nameof(shippingRates));
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public string Name { get; }

        public IEnumerable<BringShippingRate> ShippingRates { get; }
    }
}