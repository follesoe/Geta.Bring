using System.Collections.Specialized;

namespace Geta.Bring.Shipping.Model.QueryParameters
{
    /// <summary>
    /// Query parameter for CustomerNumber
    /// </summary>
    public class CustomerNumber : IShippingQueryParameter
    {
        /// <summary>
        /// Initializes new instance of <see cref="CustomerNumber"/>.
        /// </summary>
        /// <param name="customerNumber">Parameter value if edi is used.</param>
        public CustomerNumber(string customerNumber)
        {
            Items = new NameValueCollection
            {
                {"customerNumber", customerNumber}
            };
        }
        public NameValueCollection Items { get; }
    }
}