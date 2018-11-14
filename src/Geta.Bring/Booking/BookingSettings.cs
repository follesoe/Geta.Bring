using System;

namespace Geta.Bring.Booking
{
    /// <summary>
    /// Settings for <see cref="BookingClient"/>.
    /// </summary>
    public class BookingSettings
    {
        public string Uid { get; }
        public string Key { get; }
        public Uri ClientUri { get; }
        public Uri EndpointUri { get; }
        public bool IsTest { get; }

        /// <summary>
        /// Initializes new instance of <see cref="BookingSettings"/> with default endpoint URI: https://api.bring.com/booking/api/booking .
        /// </summary>
        /// <param name="uid">Booking API User ID.</param>
        /// <param name="key">Booking API Key.</param>
        /// <param name="clientUri">The URI of client Web site.</param>
        /// <param name="isTest">Mark if test mode in use.</param>
        public BookingSettings(string uid, string key, Uri clientUri, bool isTest = false)
            : this(uid, key, clientUri, new Uri("https://api.bring.com/booking/api/booking"), isTest) { }

        /// <summary>
        /// Initializes new instance of <see cref="BookingSettings"/>.
        /// </summary>
        /// <param name="uid">Booking API User ID.</param>
        /// <param name="key">Booking API Key.</param>
        /// <param name="clientUri">The URI of client Web site.</param>
        /// <param name="endpointUri">The URI of Bring Booking API endpoint.</param>
        /// <param name="isTest">Mark if test mode in use.</param>
        public BookingSettings(string uid, string key, Uri clientUri, Uri endpointUri, bool isTest = false)
        {
            if (clientUri == null) throw new ArgumentNullException(nameof(clientUri));
            if (endpointUri == null) throw new ArgumentNullException(nameof(endpointUri));

            Uid = uid ?? throw new ArgumentNullException(nameof(uid));
            Key = key ?? throw new ArgumentNullException(nameof(key));
            ClientUri = clientUri;
            EndpointUri = endpointUri;
            IsTest = isTest;
        }
    }
}