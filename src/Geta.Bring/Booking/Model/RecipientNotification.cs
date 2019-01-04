using System;

namespace Geta.Bring.Booking.Model
{
    /// <summary>
    /// Additional service on products for additional services: https://developer.bring.com/api/booking/#book-shipments .
    /// </summary>
    public class RecipientNotification
    {
        /// <summary>
        /// Initializes new instance of <see cref="RecipientNotification"/>.
        /// The recipientNotification attribute/element must be set for all products that allow electronic notification,
        /// in order for the recipient to receive electronic notification. This element must contain the recipient’s mobile phone number
        /// and/or e-mail address.
        /// </summary>
        /// <param name="email">.</param>
        /// <param name="mobile"></param>
        public RecipientNotification(string email, string mobile)
        {
            if (string.IsNullOrEmpty(email) && string.IsNullOrEmpty(mobile))
            {
                throw new ArgumentNullException("You need to specifiy either email or mobile.");
            }

            Email = email;
            Mobile = mobile;
        }

        public string Email { get; }
        public string Mobile { get; }
    }
}