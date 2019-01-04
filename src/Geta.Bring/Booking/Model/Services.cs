using System;

namespace Geta.Bring.Booking.Model
{
    /// <summary>
    /// Additional services for a booking product.
    /// </summary>
    public class Services
    {
        /// <summary>
        /// The recipient notification property must be set for all products that allow electronic notification,
        /// in order for the recipient to receive electronic notification.
        /// </summary>
        public RecipientNotification RecipientNotification { get; set; }

        /// <summary>
        /// If a sender want their package delivered to the door, and it cannot be delivered (i.e. Business closed or recipient is not present),
        /// then you can now choose what will happen with your package. Valid options can be found in the <see cref="DeliveryOption"/> class.
        /// </summary>
        public string DeliveryOption { get; set; }
    }
}