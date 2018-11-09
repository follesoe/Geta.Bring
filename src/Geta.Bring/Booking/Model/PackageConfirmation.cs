namespace Geta.Bring.Booking.Model
{
    public class PackageConfirmation
    {
        public PackageConfirmation(string correlationId, string packageNumber)
        {
            PackageNumber = packageNumber;
            CorrelationId = correlationId;
        }

        public string CorrelationId { get; }
        public string PackageNumber { get; }
    }
}