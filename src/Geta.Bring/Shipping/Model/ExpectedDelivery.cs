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
            DateTime? expectedDeliveryDate)
        {
            ExpectedDeliveryDate = expectedDeliveryDate;
            FormattedEarliestPickupDate = formattedEarliestPickupDate;
            FormattedExpectedDeliveryDate = formattedExpectedDeliveryDate;
            UserMessage = userMessage;
            WorkingDays = workingDays;
        }

        /// <summary>
        /// Description of week days when delivery available.
        /// </summary>
        public string WorkingDays { get; private set; }

        /// <summary>
        /// Message to user.
        /// </summary>
        public string UserMessage { get; private set; }

        /// <summary>
        /// Formatted expeted delivery date.
        /// </summary>
        public string FormattedExpectedDeliveryDate { get; private set; }

        /// <summary>
        /// Formatted earliest pickup date.
        /// </summary>
        public string FormattedEarliestPickupDate { get; private set; }

        /// <summary>
        /// Expected delivery date.
        /// </summary>
        [JsonConverter(typeof(DeliveryDateToDateTimeConverter))]
        public DateTime? ExpectedDeliveryDate { get; private set; }

        
    }
}