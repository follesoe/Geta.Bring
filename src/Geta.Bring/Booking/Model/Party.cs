using System;

namespace Geta.Bring.Booking.Model
{
    public class Party
    {
        public Party(
            string name, 
            string addressLine, 
            string addressLine2, 
            string postalCode, 
            string city, 
            string countryCode, 
            string reference, 
            string additionalAddressInfo, 
            Contact contact)
        {
            Contact = contact ?? throw new ArgumentNullException(nameof(contact));
            AdditionalAddressInfo = additionalAddressInfo;
            Reference = reference ?? throw new ArgumentNullException(nameof(reference));
            CountryCode = countryCode ?? throw new ArgumentNullException(nameof(countryCode));
            City = city ?? throw new ArgumentNullException(nameof(city));
            PostalCode = postalCode ?? throw new ArgumentNullException(nameof(postalCode));
            AddressLine2 = addressLine2;
            AddressLine = addressLine ?? throw new ArgumentNullException(nameof(addressLine));
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public string Name { get; }
        public string AddressLine { get; }
        public string AddressLine2 { get; }
        public string PostalCode { get; }
        public string City { get; }
        public string CountryCode { get; }
        public string Reference { get; }
        public string AdditionalAddressInfo { get; }
        public Contact Contact { get; }
    }
}