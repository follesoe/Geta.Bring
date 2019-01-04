using System;

namespace Geta.Bring.Booking.Model
{
    /// <summary>
    /// Constants for the different options for the <see cref="Services.DeliveryOption"/> property.
    /// </summary>
    public static class DeliveryOption
    {
        /// <summary>
        /// Two deliveries, then return to sender.
        /// </summary>
        public static string TwoDeliveriesThenReturn => "TWO_DELIVERIES_THEN_RETURN";

        /// <summary>
        /// One delivery, then send to Post in Butikk.
        /// </summary>
        public static string OneDeliveryThenPib => "ONE_DELIVERY_THEN_PIB";

        /// <summary>
        /// Two deliveries, then send to Post in Butikk.
        /// </summary>
        public static string TwoDeliveryThenPib => "TWO_DELIVERIES_THEN_PIB";
    }
}