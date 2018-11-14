using System;
using System.Collections.Generic;

namespace Geta.Bring.Tracking.Model
{
    /// <summary>
    /// Package consignment status.
    /// </summary>
    public class ConsignmentStatus
    {
        public ConsignmentStatus(
            string consignmentId, 
            string previousConsignmentId, 
            double totalWeightInKgs, 
            double totalVolumeInDm3, 
            IEnumerable<PackageStatus> packageSet)
        {
            PackageSet = packageSet ?? throw new ArgumentNullException(nameof(packageSet));
            TotalVolumeInDm3 = totalVolumeInDm3;
            TotalWeightInKgs = totalWeightInKgs;
            PreviousConsignmentId = previousConsignmentId ?? throw new ArgumentNullException(nameof(previousConsignmentId));
            ConsignmentId = consignmentId ?? throw new ArgumentNullException(nameof(consignmentId));
        }

        /// <summary>
        /// Consignment ID.
        /// </summary>
        public string ConsignmentId { get; }
        
        /// <summary>
        /// Previous consignment ID.
        /// </summary>
        public string PreviousConsignmentId { get; }

        /// <summary>
        /// Total weight in kilograms.
        /// </summary>
        public double TotalWeightInKgs { get; }

        /// <summary>
        /// Total volume in dm3.
        /// </summary>
        public double TotalVolumeInDm3 { get; }

        /// <summary>
        /// List of package statuses.
        /// </summary>
        public IEnumerable<PackageStatus> PackageSet { get; }

    }
}