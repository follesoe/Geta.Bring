namespace Geta.Bring.Pickup.Model
{
    public class PickupPoint
    {
        public PickupPoint(
           string id,
           string unitId,
           string name,
           string address,
           string postalCode,
           string city,
           string countryCode,
           string municipality,
           string county,
           string visitingAddress,
           string visitingPostalCode,
           string visitingCity,
           string locationDescription,
           string openingHoursNorwegian,
           string openingHoursEnglish,
           string openingHoursFinnish,
           string openingHoursSwedish,
           string openingHoursDanish,
           double latitude,
           double longitude,
           string utmX,
           string utmY,
           string postenMapsLink,
           string googleMapsLink,
           string distanceInKm,
           string distanceType,
           string type,
           string additionalServiceCode,
           string routeMapsLink)
        {
            Id = id;
            UnitId = unitId;
            Name = name;
            Address = address;
            PostalCode = postalCode;
            City = city;
            CountryCode = countryCode;
            Municipality = municipality;
            Country = county;
            VisitingAddress = visitingAddress;
            VisitingPostalCode = visitingPostalCode;
            VisitingCity = visitingCity;
            LocationDescription = locationDescription;
            OpeningHoursNorwegian = openingHoursNorwegian;
            OpeningHoursEnglish = openingHoursEnglish;
            OpeningHoursFinnish = openingHoursFinnish;
            OpeningHoursSwedish = openingHoursSwedish;
            OpeningHoursDanish = openingHoursDanish;
            Latitude = latitude;
            Longitude = longitude;
            UtmX = utmX;
            UtmY = utmY;
            PostenMapsLink = postenMapsLink;
            GoogleMapsLink = googleMapsLink;
            DistanceInKm = distanceInKm;
            DistanceType = distanceType;
            Type = type;
            AdditionalServiceCode = additionalServiceCode;
            RouteMapsLink = routeMapsLink;
        }

        public string Id { get; }

        public string UnitId { get; }

        public string Name { get; }

        public string Address { get; }

        public string PostalCode { get; }

        public string City { get; }

        public string CountryCode { get; }

        public string Municipality { get; }

        public string Country { get; }

        public string VisitingAddress { get; }

        public string VisitingPostalCode { get; }

        public string VisitingCity { get; }

        public string LocationDescription { get; }

        public string OpeningHoursNorwegian { get; }

        public string OpeningHoursEnglish { get; }

        public string OpeningHoursFinnish { get; }

        public string OpeningHoursDanish { get; }

        public string OpeningHoursSwedish { get; }

        public double Latitude { get; }

        public double Longitude { get; }

        public string UtmX { get; }

        public string UtmY { get; }

        public string PostenMapsLink { get; }

        public string GoogleMapsLink { get; }

        public string DistanceInKm { get; }

        public string DistanceType { get; }

        public string Type { get; }

        public string AdditionalServiceCode { get; }

        public string RouteMapsLink { get; }
    }
}