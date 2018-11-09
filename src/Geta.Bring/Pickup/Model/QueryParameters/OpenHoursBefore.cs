using System.Collections.Specialized;

namespace Geta.Bring.Pickup.Model.QueryParameters
{
    /// <summary>
    /// A filter for pickup points on or before a specific time
    /// </summary>
    public class OpenHoursBefore : IPickupQueryParameter
    {
        private const string ParameterName = "openOnOrBefore";

        /// <summary>
        /// Initialize a new filter for opening hours before
        /// </summary>
        /// <param name="time">Timestamp to be open on or before <see cref="Time"/></param>
        public OpenHoursBefore(Time time)
        {
            Items = new NameValueCollection
            {
                { ParameterName, time.ToString() }
            };
        }

        public NameValueCollection Items { get; }
    }
}