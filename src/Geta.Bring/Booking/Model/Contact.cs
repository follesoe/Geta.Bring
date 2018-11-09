using System;

namespace Geta.Bring.Booking.Model
{
    public class Contact
    {
        public Contact(string name, string email, string phoneNumber)
        {
            PhoneNumber = phoneNumber ?? throw new ArgumentNullException(nameof(phoneNumber));
            Email = email ?? throw new ArgumentNullException(nameof(email));
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public string Name { get; }
        public string Email { get; }
        public string PhoneNumber { get; }
    }
}