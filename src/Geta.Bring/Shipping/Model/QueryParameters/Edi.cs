using System.Collections.Specialized;

namespace Geta.Bring.Shipping.Model.QueryParameters
{
    /// <summary>
    /// Query parameter to describe if EDI is used.
    /// </summary>
    public class Edi : IShippingQueryParameter
    {
        /// <summary>
        /// Initializes new instance of <see cref="Edi"/>.
        /// </summary>
        /// <param name="usesEdi">Parameter value if edi is used.</param>
        public Edi(bool usesEdi)
        {
            Items = new NameValueCollection
            {
                {"edi", usesEdi.ToString().ToLowerInvariant()}
            };
        }
        public NameValueCollection Items { get; }
    }
}