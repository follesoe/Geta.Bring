using System;
using System.Collections.Specialized;
using System.Linq;

namespace Geta.Bring.Shipping.Model.QueryParameters
{
    /// <summary>
    /// Query parameter to describe required additional services of type <see cref="AdditionalService"/>.
    /// </summary>
    public class AdditionalServices : IShippingQueryParameter
    {
        private const string ParameterName = "additional";

        /// <summary>
        /// Initializes new instance of <see cref="AdditionalServices"/>.
        /// </summary>
        /// <param name="additionalServices">Parameter list of <see cref="AdditionalService"/>.</param>
        public AdditionalServices(params AdditionalService[] additionalServices)
        {
            var services = additionalServices.ToList();
            services.ForEach(x =>
            {
                if (x == null)
                    throw new ArgumentException("additionalServices contains null item", nameof(additionalServices));
            });

            Items = new NameValueCollection();

            services
                .ForEach(x => Items.Add(ParameterName, x.Code));
        }

        public NameValueCollection Items { get; }
    }
}