using System;
using System.Collections.Generic;
using System.Linq;

namespace Geta.Bring.Tracking.Model
{
    /// <summary>
    /// Tracking event.
    /// </summary>
    public class TrackingEvent
    {
        public TrackingEvent(
            string description, 
            string status, 
            RecipientSignature recipientSignature, 
            string unitId, 
            Uri unitInformationUrl, 
            string unitType, 
            string postalCode, 
            string city, 
            string countryCode, 
            string country, 
            DateTime dateIso, 
            string displayDate, 
            string displayTime, 
            bool consignmentEvent, 
            IEnumerable<TrackingEventDefinition> definitions)
        {
            Definitions = definitions ?? Enumerable.Empty<TrackingEventDefinition>();
            ConsignmentEvent = consignmentEvent;
            DisplayTime = displayTime ?? throw new ArgumentNullException(nameof(displayTime));
            DisplayDate = displayDate ?? throw new ArgumentNullException(nameof(displayDate));
            DateIso = dateIso;
            Country = country ?? throw new ArgumentNullException(nameof(country));
            CountryCode = countryCode ?? throw new ArgumentNullException(nameof(countryCode));
            City = city ?? throw new ArgumentNullException(nameof(city));
            PostalCode = postalCode ?? throw new ArgumentNullException(nameof(postalCode));
            UnitType = unitType ?? throw new ArgumentNullException(nameof(unitType));
            UnitInformationUrl = unitInformationUrl;
            UnitId = unitId ?? throw new ArgumentNullException(nameof(unitId));
            RecipientSignature = recipientSignature ?? throw new ArgumentNullException(nameof(recipientSignature));
            Status = status ?? throw new ArgumentNullException(nameof(status));
            Description = description ?? throw new ArgumentNullException(nameof(description));
        }

        /// <summary>
        /// Description of tracking event.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Event status.
        /// </summary>
        public string Status { get; }

        /// <summary>
        /// Recipient signature.
        /// </summary>
        public RecipientSignature RecipientSignature { get; }

        /// <summary>
        /// Unit ID.
        /// </summary>
        public string UnitId { get; }

        /// <summary>
        /// Unit information URI.
        /// </summary>
        public Uri UnitInformationUrl { get; }

        /// <summary>
        /// Unit type.
        /// </summary>
        public string UnitType { get; }

        public string PostalCode { get; }
        public string City { get; }
        public string CountryCode { get; }
        public string Country { get; }

        /// <summary>
        /// Event date and time.
        /// </summary>
        public DateTime DateIso { get; }

        /// <summary>
        /// Formatted event date.
        /// </summary>
        public string DisplayDate { get; }

        /// <summary>
        /// Formatted event time.
        /// </summary>
        public string DisplayTime { get; }

        /// <summary>
        /// Mark if it is consignment event.
        /// </summary>
        public bool ConsignmentEvent { get; }

        /// <summary>
        /// List of the tracking event definitions.
        /// </summary>
        public IEnumerable<TrackingEventDefinition> Definitions { get; }
    }
}