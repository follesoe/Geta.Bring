using System.Collections.Generic;

namespace Geta.Bring.Booking.Model.Dtos
{
    public class BookingRequest
    {
        public bool TestIndicator { get; set; }
        public int SchemaVersion { get; set; }
        public IEnumerable<Consignment> Consignments { get; set; }
    }
}