using System.Collections.Specialized;

namespace Geta.Bring.Pickup.Model.QueryParameters
{
    /// <summary>
    /// A filter for pickup points open a specific day
    /// </summary>
    public class OpenOnDays : IPickupQueryParameter
    {
        private const string ParameterName = "openingHoursSearchType";

        /// <summary>
        /// Initialize a new filter for pickup points open on specific days
        /// </summary>
        /// <param name="filter">A filter of <see cref="OpenDaysFilter"/></param>
        public OpenOnDays(OpenDaysFilter filter)
        {
            Items = new NameValueCollection
            {
                { ParameterName, filter.ToString().ToUpperInvariant() }
            };
        }

        public NameValueCollection Items { get; }
    }
}