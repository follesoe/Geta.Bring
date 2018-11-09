using System;

namespace Geta.Bring.Booking.Model
{
    /// <summary>
    /// Sender and recipient party information.
    /// </summary>
    public class Parties
    {
        /// <summary>
        /// Initializes new instance of <see cref="Parties"/>.
        /// </summary>
        /// <param name="sender">Package sender.</param>
        /// <param name="recipient">Package recipient.</param>
        public Parties(Party sender, Party recipient)
        {
            Recipient = recipient ?? throw new ArgumentNullException(nameof(recipient));
            Sender = sender ?? throw new ArgumentNullException(nameof(sender));
        }

        /// <summary>
        /// Package sender. 
        /// </summary>
        public Party Sender { get; }

        /// <summary>
        /// Package recipient.
        /// </summary>
        public Party Recipient { get; }
    }
}