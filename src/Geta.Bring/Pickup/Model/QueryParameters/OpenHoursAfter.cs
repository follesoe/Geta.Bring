using System.Collections.Specialized;

namespace Geta.Bring.Pickup.Model.QueryParameters
{
    /// <summary>
    /// A filter for pickup points open on or after a specific time
    /// </summary>
    public class OpenHoursAfter : IPickupQueryParameter
    {
        private const string ParameterName = "openOnOrAfter";

        /// <summary>
        /// Initialize a new filter for opening hours after
        /// </summary>
        /// <param name="time">Timestamp to be open on or after <see cref="Time"/></param>
        public OpenHoursAfter(Time time)
        {
            Items = new NameValueCollection
            {
                { ParameterName, time.ToString() }
            };
        }

        public NameValueCollection Items { get; }
    }
}