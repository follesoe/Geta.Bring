using System;

namespace Geta.Bring.Booking.Model
{
    public class ErrorMessage
    {
        public ErrorMessage(string lang, string message)
        {
            Message = message ?? throw new ArgumentNullException(nameof(message));
            Lang = lang ?? throw new ArgumentNullException(nameof(lang));
        }

        public string Lang { get; }
        public string Message { get; }
    }
}