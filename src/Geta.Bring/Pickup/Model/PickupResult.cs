using System;
using System.Collections.Generic;
using System.Linq;

namespace Geta.Bring.Pickup.Model
{
    public class PickupResult
    {
        public PickupResult()
        {
            PickupPoints = Enumerable.Empty<PickupPoint>();
            ErrorMessages = Enumerable.Empty<string>();    
        }

        /// <summary>
        /// List of pickup points.
        /// </summary>
        public IEnumerable<PickupPoint> PickupPoints { get; private set; }

        public bool Success { get; private set; }
        public IEnumerable<string> ErrorMessages { get; private set; }

        /// <summary>
        /// Initializes new instance of <see cref="PickupResult"/> with successful status.
        /// </summary>
        /// <param name="pickupPoints">List of pickup points.</param>
        public static PickupResult CreateSuccess(IEnumerable<PickupPoint> pickupPoints)
        {
            if (pickupPoints == null) throw new ArgumentNullException(nameof(pickupPoints));
            return new PickupResult
            {
                PickupPoints = pickupPoints,
                Success = true
            };
        }


        /// <summary>
        /// Initializes new instance of <see cref="PickupResult"/> with failure status.
        /// </summary>
        /// <param name="message">Error message.</param>
        public static PickupResult CreateFailure(string message)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));

            return new PickupResult
            {
                Success = false,
                ErrorMessages = new[] { message }
            };
        }
    }
}