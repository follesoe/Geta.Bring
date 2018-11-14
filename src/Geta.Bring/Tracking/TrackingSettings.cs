using System;

namespace Geta.Bring.Tracking
{
    /// <summary>
    /// Settings for <see cref="TrackingClient"/>.
    /// </summary>
    public class TrackingSettings
    {
        /// <summary>
        /// Initializes new instance of <see cref="TrackingSettings"/> with default endpoint URI: http://sporing.bring.no/sporing.json 
        /// </summary>
        public TrackingSettings()
            : this(new Uri("http://sporing.bring.no/sporing.json")) { }

        /// <summary>
        /// Initializes new instance of <see cref="TrackingSettings"/>.
        /// </summary>
        /// <param name="endpointUri">Bring Tracking API endpoint URI.</param>
        public TrackingSettings(Uri endpointUri)
        {
            if (endpointUri == null) throw new ArgumentNullException(nameof(endpointUri));
            EndpointUri = endpointUri;
        }

        public Uri EndpointUri { get; }
    }
}