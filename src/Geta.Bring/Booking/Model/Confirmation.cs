using System;
using System.Collections.Generic;
using System.Linq;

namespace Geta.Bring.Booking.Model
{
    public class Confirmation
    {
        protected Confirmation(bool success)
        {
            Errors = Enumerable.Empty<Error>();
            Success = success;
            Packages = Enumerable.Empty<PackageConfirmation>();
        }

        public static Confirmation CreateSuccess(
            string correlationId,
            string consignmentNumber,
            Uri labelsLink,
            Uri trackingLink,
            DateTime? earliestPickup,
            DateTime? expectedDelivery,
            IEnumerable<PackageConfirmation> packages)
        {
            return new Confirmation(true)
            {
                CorrelationId = correlationId,
                ConsignmentNumber = consignmentNumber,
                LabelsLink = labelsLink,
                TrackingLink = trackingLink,
                EarliestPickup = earliestPickup,
                ExpectedDelivery = expectedDelivery,
                Packages = packages
            };
        }

        public static Confirmation CreateError(IEnumerable<Error> errors)
        {
            return new Confirmation(false)
            {
                Errors = errors
            };
        }

        public static Confirmation CreateError(string message, string lang = "en")
        {
            var error = new Error(string.Empty, string.Empty, new[] {new ErrorMessage(lang, message)});
            return CreateError(new[] {error});
        }

        public bool Success { get; }
        public IEnumerable<Error> Errors { get; private set; }

        public string CorrelationId { get; private set; }
        public string ConsignmentNumber { get; private set; }
        public Uri LabelsLink { get; private set; }
        public Uri TrackingLink { get; private set; }
        public DateTime? EarliestPickup { get; private set; }
        public DateTime? ExpectedDelivery { get; private set; }
        public IEnumerable<PackageConfirmation> Packages { get; private set; }
    }
}