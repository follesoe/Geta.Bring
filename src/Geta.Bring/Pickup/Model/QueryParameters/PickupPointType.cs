using System.Collections.Specialized;

namespace Geta.Bring.Pickup.Model.QueryParameters
{
    /// <summary>
    /// A filter for pickup points of a specific type
    /// </summary>
    public class PickupPointType : IPickupQueryParameter
    {
        private const string ParameterName = "pickupPointType";

        /// <summary>
        /// Initialize a new filter for pickup points of specific type
        /// </summary>
        /// <param name="filter">A filter of <see cref="PickupPointTypeFilter"/></param>
        public PickupPointType(PickupPointTypeFilter filter)
        {
            Items = new NameValueCollection
            {
                { ParameterName, filter.ToString().ToLowerInvariant() }
            };
        }

        public NameValueCollection Items { get; }
    }
}