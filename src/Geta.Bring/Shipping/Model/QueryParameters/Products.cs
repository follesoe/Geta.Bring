using System;
using System.Collections.Specialized;
using System.Linq;

namespace Geta.Bring.Shipping.Model.QueryParameters
{
    /// <summary>
    /// Query parameter to describe required products of type <see cref="Product"/>.
    /// </summary>
    public class Products : IShippingQueryParameter
    {
        private const string ParameterName = "product";

        /// <summary>
        /// Initializes new instance of <see cref="Products"/>.
        /// </summary>
        /// <param name="additionalProducts">Parameter list of <see cref="Product"/>.</param>
        public Products(params Product[] additionalProducts)
        {
            var products = additionalProducts.ToList();
            products.ForEach(x =>
            {
                if (x == null)
                    throw new ArgumentException("additionalProducts contains null item", nameof(additionalProducts));
            });

            Items = new NameValueCollection();

            products
                .ForEach(x => Items.Add(ParameterName, x.Code));
        }

        public NameValueCollection Items { get; }
    }
}