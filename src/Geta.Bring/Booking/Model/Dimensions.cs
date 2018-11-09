namespace Geta.Bring.Booking.Model
{
    /// <summary>
    /// Dimensions information.
    /// </summary>
    public class Dimensions
    {
        /// <summary>
        /// Initializes new instance of <see cref="Dimensions"/>.
        /// </summary>
        /// <param name="heightInCm">Height in cm.</param>
        /// <param name="widthInCm">Width in cm.</param>
        /// <param name="lengthInCm">Length in cm.</param>
        public Dimensions(int heightInCm, int widthInCm, int lengthInCm)
        {
            LengthInCm = lengthInCm;
            WidthInCm = widthInCm;
            HeightInCm = heightInCm;
        }

        /// <summary>
        /// Height in cm.
        /// </summary>
        public int HeightInCm { get; }

        /// <summary>
        /// Width in cm.
        /// </summary>
        public int WidthInCm { get; }

        /// <summary>
        /// Length in cm.
        /// </summary>
        public int LengthInCm { get; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;
            return Equals((Dimensions)obj);
        }

        protected bool Equals(Dimensions other)
        {
            return HeightInCm == other.HeightInCm && WidthInCm == other.WidthInCm && LengthInCm == other.LengthInCm;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = HeightInCm;
                hashCode = (hashCode*397) ^ WidthInCm;
                hashCode = (hashCode*397) ^ LengthInCm;
                return hashCode;
            }
        }
    }
}