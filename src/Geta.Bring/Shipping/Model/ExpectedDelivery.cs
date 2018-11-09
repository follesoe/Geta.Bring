using System;
using System.Collections.Generic;
using System.Linq;
using Geta.Bring.Shipping.Infrastructure;
using Newtonsoft.Json;

namespace Geta.Bring.Shipping.Model
{
    /// <summary>
    /// Expected delivery information.
    /// </summary>
    public class ExpectedDelivery
    {
        public ExpectedDelivery(
            string workingDays, 
            string userMessage, 
            string formattedExpectedDeliveryDate, 
            string formattedEarliestPickupDate,
            DateTime? expectedDeliveryDate,
            IEnumerable<DateTime> alternativeDeliveryDates)
        {
            AlternativeDeliveryDates = alternativeDeliveryDates ?? Enumerable.Empty<DateTime>();
            ExpectedDeliveryDate = expectedDeliveryDate;
            FormattedEarliestPickupDate = formattedEarliestPickupDate;
            FormattedExpectedDeliveryDate = formattedExpectedDeliveryDate;
            UserMessage = userMessage;
            WorkingDays = workingDays;
        }

        /// <summary>
        /// Description of week days when delivery available.
        /// </summary>
        public string WorkingDays { get; }

        /// <summary>
        /// Message to user.
        /// </summary>
        public string UserMessage { get; }

        /// <summary>
        /// Formatted expeted delivery date.
        /// </summary>
        public string FormattedExpectedDeliveryDate { get; }

        /// <summary>
        /// Formatted earliest pickup date.
        /// </summary>
        public string FormattedEarliestPickupDate { get; }

        /// <summary>
        /// Expected delivery date.
        /// </summary>
        [JsonConverter(typeof(DeliveryDateToDateTimeConverter))]
        public DateTime? ExpectedDeliveryDate { get; }

        /// <summary>
        /// List of alternative expected delivery dates.
        /// </summary>
        public IEnumerable<DateTime> AlternativeDeliveryDates { get; }
    }
}