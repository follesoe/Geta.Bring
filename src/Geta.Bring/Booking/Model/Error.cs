using System;
using System.Collections.Generic;

namespace Geta.Bring.Booking.Model
{
    public class Error
    {
        public Error(string uniqueId, string code, IEnumerable<ErrorMessage> messages)
        {
            Messages = messages ?? throw new ArgumentNullException(nameof(messages));
            Code = code ?? throw new ArgumentNullException(nameof(code));
            UniqueId = uniqueId ?? throw new ArgumentNullException(nameof(uniqueId));
        }

        public string UniqueId { get; }
        public string Code { get; }
        public IEnumerable<ErrorMessage> Messages { get; }
    }
}