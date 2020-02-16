using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Geta.Bring.Booking.Infrastructure;
using Geta.Bring.Booking.Mapping;
using Geta.Bring.Booking.Model;
using Geta.Bring.Booking.Model.Dtos;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Geta.Bring.Booking
{
    /// <summary>
    /// Bring Booking API client.
    /// </summary>
    public class BookingClient : IBookingClient
    {
        public BookingSettings Settings { get; }

        public BookingClient(BookingSettings settings)
        {
            Settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        /// <summary>
        /// Single consignment booking.
        /// </summary>
        /// <param name="consignment">Consignment to book.</param>
        /// <returns>Booking confirmation.</returns>
        public async Task<Confirmation> BookAsync(Consignment consignment)
        {
            var consignments = await BookAsync(new[] {consignment}).ConfigureAwait(false);
            var first = consignments.FirstOrDefault();
            return first ?? Confirmation.CreateError("No confirmation received.");
        }

        /// <summary>
        /// Multiple consignment booking.
        /// </summary>
        /// <param name="consignments">List of consignments.</param>
        /// <returns>List of booking confirmations.</returns>
        public async Task<IEnumerable<Confirmation>> BookAsync(IEnumerable<Consignment> consignments)
        {
            using (var client = CreateClient())
            {
                var settings = new JsonSerializerSettings
                {
                    Converters = new JsonConverter[] {new MilisecondEpochConverter()},
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };
                var stringRequest = JsonConvert.SerializeObject(consignments.ToRequest(Settings.IsTest), settings);
                var requestContent = new StringContent(stringRequest, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(Settings.EndpointUri, requestContent).ConfigureAwait(false);
                var stringResponse = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                if (!response.IsSuccessStatusCode && !string.IsNullOrWhiteSpace(stringResponse))
                {
                    var statusCode = $"{(int)response.StatusCode} ({response.StatusCode.ToString()})";
                    throw new HttpRequestException($"{statusCode}: {stringResponse}");
                }
                else
                {
                    response.EnsureSuccessStatusCode();
                }

                return JsonConvert.DeserializeObject<BookingResponse>(stringResponse, new MilisecondEpochConverter())
                    .ToConfirmation();
            }
        }

        private HttpClient CreateClient()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("X-MyBring-API-Uid", Settings.Uid);
            client.DefaultRequestHeaders.Add("X-MyBring-API-Key", Settings.Key);
            client.DefaultRequestHeaders.Add("X-Bring-Client-URL", Settings.ClientUri.ToString());
            return client;
        }
    }
}