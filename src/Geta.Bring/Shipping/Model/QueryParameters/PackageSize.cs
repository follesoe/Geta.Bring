using System.Collections.Specialized;
using System.Globalization;

namespace Geta.Bring.Shipping.Model.QueryParameters
{
    /// <summary>
    /// Query parameter to describe package size.
    /// </summary>
    public class PackageSize : IShippingQueryParameter
    {
        private PackageSize() { }

        public NameValueCollection Items { get; private set; }

        /// <summary>
        /// Initializes new instance of <see cref="PackageSize"/> in grams.
        /// </summary>
        /// <param name="grams">Package size in grams.</param>
        /// <returns>New instance of <see cref="PackageSize"/>.</returns>
        public static PackageSize InGrams(int grams)
        {
            return new PackageSize
            {
                Items = new NameValueCollection
                {
                    {"weight", grams.ToString(CultureInfo.InvariantCulture)}
                }
            };
        }

        /// <summary>
        /// Initializes new instance of <see cref="PackageSize"/> with provided dimensions. 
        /// </summary>
        /// <param name="lengthCm">Length in cm.</param>
        /// <param name="widthCm">Width in cm.</param>
        /// <param name="heightCm">Height in cm.</param>
        /// <returns>New instance of <see cref="PackageSize"/>.</returns>
        public static PackageSize InDimensions(int lengthCm, int widthCm, int heightCm)
        {
            return new PackageSize
            {
                Items = new NameValueCollection
                {
                    {"length", lengthCm.ToString(CultureInfo.InvariantCulture)},
                    {"width", widthCm.ToString(CultureInfo.InvariantCulture)},
                    {"height", heightCm.ToString(CultureInfo.InvariantCulture)}
                }
            }; 
        }

        /// <summary>
        /// Initializes new instance of <see cref="PackageSize"/> with provided volume.  
        /// </summary>
        /// <param name="volumeDm3">Volume in dm3.</param>
        /// <returns>New instance of <see cref="PackageSize"/>.</returns>
        public static PackageSize InVolume(int volumeDm3)
        {
            return new PackageSize
            {
                Items = new NameValueCollection
                {
                    {"volumeindm3", volumeDm3.ToString(CultureInfo.InvariantCulture)}
                }
            };
        }
    }
}