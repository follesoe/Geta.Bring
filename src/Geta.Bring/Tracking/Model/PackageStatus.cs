using System;
using System.Collections.Generic;

namespace Geta.Bring.Tracking.Model
{
    /// <summary>
    /// Package status.
    /// </summary>
    public class PackageStatus
    {
        public PackageStatus(
            string statusDescription, 
            string packageNumber, 
            string previousPackageNumber, 
            string productName, 
            string productCode, 
            string brand, 
            int lengthInCm, 
            int widthInCm, 
            int heightInCm, 
            double volumeInDm3, 
            double weightInKgs, 
            string pickupCode, 
            string dateOfReturn, 
            string senderName, 
            Address recipientAddress, 
            IEnumerable<TrackingEvent> eventSet)
        {
            EventSet = eventSet ?? throw new ArgumentNullException(nameof(eventSet));
            RecipientAddress = recipientAddress ?? Address.Empty;
            SenderName = senderName;
            DateOfReturn = dateOfReturn;
            PickupCode = pickupCode;
            WeightInKgs = weightInKgs;
            VolumeInDm3 = volumeInDm3;
            HeightInCm = heightInCm;
            WidthInCm = widthInCm;
            LengthInCm = lengthInCm;
            Brand = brand ?? throw new ArgumentNullException(nameof(brand));
            ProductCode = productCode ?? throw new ArgumentNullException(nameof(productCode));
            ProductName = productName ?? throw new ArgumentNullException(nameof(productName));
            PreviousPackageNumber = previousPackageNumber ?? throw new ArgumentNullException(nameof(previousPackageNumber));
            PackageNumber = packageNumber ?? throw new ArgumentNullException(nameof(packageNumber));
            StatusDescription = statusDescription ?? throw new ArgumentNullException(nameof(statusDescription));
        }

        /// <summary>
        /// Status description.
        /// </summary>
        public string StatusDescription { get; }

        /// <summary>
        /// Package number.
        /// </summary>
        public string PackageNumber { get; }

        /// <summary>
        /// Previous package number.
        /// </summary>
        public string PreviousPackageNumber { get; }

        /// <summary>
        /// Product name.
        /// </summary>
        public string ProductName { get; }

        /// <summary>
        /// Product code.
        /// </summary>
        public string ProductCode { get; }

        /// <summary>
        /// Brand name.
        /// </summary>
        public string Brand { get; }

        /// <summary>
        /// Length of the package in cm.
        /// </summary>
        public int LengthInCm { get; }

        /// <summary>
        /// Width of the package in cm.
        /// </summary>
        public int WidthInCm { get; }

        /// <summary>
        /// Height of the package in cm.
        /// </summary>
        public int HeightInCm { get; }

        /// <summary>
        /// Volume of the package in dm3.
        /// </summary>
        public double VolumeInDm3 { get; }

        /// <summary>
        /// Weight of the package in kilograms.
        /// </summary>
        public double WeightInKgs { get; }

        /// <summary>
        /// Pickup code.
        /// </summary>
        public string PickupCode { get; }

        /// <summary>
        /// Formatted date of return.
        /// </summary>
        public string DateOfReturn { get; }

        /// <summary>
        /// Sender name.
        /// </summary>
        public string SenderName { get; }

        /// <summary>
        /// Recipient address.
        /// </summary>
        public Address RecipientAddress { get; }

        /// <summary>
        /// List of the tracking events.
        /// </summary>
        public IEnumerable<TrackingEvent> EventSet { get; }
    }
}