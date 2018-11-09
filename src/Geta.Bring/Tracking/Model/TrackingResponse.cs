using System;
using System.Collections.Generic;

namespace Geta.Bring.Tracking.Model
{
    internal class TrackingResponse
    {
        public TrackingResponse(IEnumerable<ConsignmentStatus> consignmentSet)
        {
            ConsignmentSet = consignmentSet ?? throw new ArgumentNullException(nameof(consignmentSet));
        }

        public IEnumerable<ConsignmentStatus> ConsignmentSet { get; }
    }
}