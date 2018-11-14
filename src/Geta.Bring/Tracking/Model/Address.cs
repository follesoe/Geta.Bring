using System;

namespace Geta.Bring.Tracking.Model
{
    public class Address
    {
        public Address(
            string addressLine1, 
            string addressLine2, 
            string postalCode, 
            string city,
            string countryCode, 
            string country)
        {
            Country = country ?? throw new ArgumentNullException(nameof(country));
            CountryCode = countryCode ?? throw new ArgumentNullException(nameof(countryCode));
            City = city ?? throw new ArgumentNullException(nameof(city));
            PostalCode = postalCode ?? throw new ArgumentNullException(nameof(postalCode));
            AddressLine2 = addressLine2 ?? throw new ArgumentNullException(nameof(addressLine2));
            AddressLine1 = addressLine1 ?? throw new ArgumentNullException(nameof(addressLine1));
        }

        public string AddressLine1 { get; }
        public string AddressLine2 { get; }
        public string PostalCode { get; }
        public string City { get; }
        public string CountryCode { get; }
        public string Country { get; }

        public static Address Empty = 
            new Address(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
    }
}