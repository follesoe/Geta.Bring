using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Geta.Bring.Tracking.Model;
using Newtonsoft.Json;

namespace Geta.Bring.Tracking
{
    /// <summary>
    /// Bring Tracking API client.
    /// </summary>
    public class TrackingClient : ITrackingClient
    {
        /// <summary>
        /// Initializes new instance of <see cref="TrackingClient"/> with default <see cref="TrackingSettings()"/>.
        /// </summary>
        public TrackingClient()
        {
            Settings = new TrackingSettings();
        }

        /// <summary>
        /// Initializes new instance of <see cref="TrackingClient"/>.
        /// </summary>
        /// <param name="settings">Settings for <see cref="TrackingClient"/></param>
        public TrackingClient(TrackingSettings settings)
        {
            Settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        public TrackingSettings Settings { get; }

        /// <summary>
        /// Returns tracking information.
        /// </summary>
        /// <param name="trackingNumber">Tracking number.</param>
        /// <returns>List of package consignment statuses. See more: <see cref="ConsignmentStatus"/>.</returns>
        public async Task<IEnumerable<ConsignmentStatus>> TrackAsync(string trackingNumber)
        {
            using (var client = CreateClient())
            {
                var requestUri = CreateRequestUri(trackingNumber);
                var jsonResponse = await client.GetStringAsync(requestUri).ConfigureAwait(false);
                var response = JsonConvert.DeserializeObject<TrackingResponse>(jsonResponse);
                return response.ConsignmentSet;
            }
        }

        private Uri CreateRequestUri(string trackingNumber)
        {
            var ub = new UriBuilder(Settings.EndpointUri);
            var queryValues = HttpUtility.ParseQueryString(Settings.EndpointUri.Query);
            queryValues.Add("q", trackingNumber);
            ub.Query = queryValues.ToString();
            return ub.Uri;
        }

        private HttpClient CreateClient()
        {
            var client = new HttpClient();
            return client;
        }
    }
}