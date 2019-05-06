using System;

namespace Geta.Bring.Booking.Model
{
    /// <summary>
    /// Package information to book.
    /// </summary>
    public class Package
    {
        /// <summary>
        /// Initializes new instance of <see cref="Package"/>.
        /// </summary>
        /// <param name="correlationId">Correlation ID.</param>
        /// <param name="weightInKg">Package weight in kilograms.</param>
        /// <param name="goodsDescription">Package goods description.</param>
        /// <param name="dimensions">Package dimensions. For more see: <see cref="Dimensions"/>.</param>
        public Package(
            string correlationId, 
            double weightInKg, 
            string goodsDescription, 
            Dimensions dimensions)
        {
            Dimensions = dimensions ?? throw new ArgumentNullException(nameof(dimensions));
            GoodsDescription = goodsDescription ?? throw new ArgumentNullException(nameof(goodsDescription));
            WeightInKg = weightInKg;
            CorrelationId = correlationId ?? throw new ArgumentNullException(nameof(correlationId));
        }

        /// <summary>
        /// Correlation ID.
        /// </summary>
        public string CorrelationId { get; }

        /// <summary>
        /// Package weight in kilograms.
        /// </summary>
        public double WeightInKg { get; }

        /// <summary>
        /// Package goods description.
        /// </summary>
        public string GoodsDescription { get; }

        /// <summary>
        /// Package dimensions. For more see: <see cref="Dimensions"/>.
        /// </summary>
        public Dimensions Dimensions { get; }
    }
}