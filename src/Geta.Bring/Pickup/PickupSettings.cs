using System;

namespace Geta.Bring.Pickup
{
    /// <summary>
    /// Settings for <see cref="PickupClient" /> 
    /// </summary>
    public class PickupSettings
    {
        // <summary>
        /// Initializes new instance of <see cref="PickupSettings"/> with default Bring Shipping Guide API endpoint: https://api.bring.com/pickuppoint/api/pickuppoint/
        /// </summary>
        /// <param name="clientUri">The URI of client Web site.</param>
        public PickupSettings()
            : this(new Uri("https://api.bring.com/pickuppoint/api/pickuppoint/")) { }


        public PickupSettings(
           Uri endpointUri)
        {
            if (endpointUri == null) throw new ArgumentNullException(nameof(endpointUri));

            EndpointUri = endpointUri;
        }

        public Uri EndpointUri { get; }
    }
}