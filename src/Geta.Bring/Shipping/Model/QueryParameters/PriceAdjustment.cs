using System;
using System.Collections.Specialized;
using System.Globalization;

namespace Geta.Bring.Shipping.Model.QueryParameters
{
    /// <summary>
    /// Query parameter to describe price adjustments.
    /// </summary>
    [Obsolete("This parameter is no longer used by the Bring API", true)]
    public class PriceAdjustment : IShippingQueryParameter
    {
        private const string ParameterName = "priceAdjustment";
        private const string IncreaseFormat = "p{0}";
        private const string DecreaseFormat = "m{0}";
        private const string PercentFormat = "{0}p";
        private const string ProductFormat = "{0}_{1}";

        private PriceAdjustment() { }

        public NameValueCollection Items { get; private set; }

        /// <summary>
        /// Initializes new instance of <see cref="PriceAdjustment"/> with fixed price.
        /// </summary>
        /// <param name="fixedPrice">Price in NOK.</param>
        /// <returns>New instance of <see cref="PriceAdjustment"/>.</returns>
        public static PriceAdjustment Fixed(int fixedPrice)
        {
            return NewWithParameter(fixedPrice.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Initializes new instance of <see cref="PriceAdjustment"/> for product with fixed price.
        /// </summary>
        /// <param name="product">Product for which to adjust price.</param>
        /// <param name="fixedPrice">Price in NOK.</param>
        /// <returns>New instance of <see cref="PriceAdjustment"/>.</returns>
        public static PriceAdjustment Fixed(Product product, int fixedPrice)
        {
            return NewWithParameter(AsProductValue(product, fixedPrice.ToString(CultureInfo.InvariantCulture)));
        }

        /// <summary>
        /// Initializes new instance of <see cref="PriceAdjustment"/> to increase price by amount of NOK.
        /// </summary>
        /// <param name="amount">Value in NOK by which to adjust price.</param>
        /// <returns>New instance of <see cref="PriceAdjustment"/>.</returns>
        public static PriceAdjustment Increase(int amount)
        {
            return NewWithParameter(AsIncreaseValue(amount)); 
        }

        /// <summary>
        /// Initializes new instance of <see cref="PriceAdjustment"/> for product to increase price by amount of NOK. 
        /// </summary>
        /// <param name="product">Product for which to adjust price.</param>
        /// <param name="amount">Value in NOK by which to adjust price.</param>
        /// <returns>New instance of <see cref="PriceAdjustment"/>.</returns>
        public static PriceAdjustment Increase(Product product, int amount)
        {
            return NewWithParameter(AsProductValue(product, AsIncreaseValue(amount)));
        }

        /// <summary>
        /// Initializes new instance of <see cref="PriceAdjustment"/> to increase price by percent. 
        /// </summary>
        /// <param name="percent">Value in percent by which to adjust price.</param>
        /// <returns>New instance of <see cref="PriceAdjustment"/>.</returns>
        public static PriceAdjustment IncreasePercent(int percent)
        {
            return NewWithParameter(AsIncreaseValue(AsPercentValue(percent)));
        }

        /// <summary>
        /// Initializes new instance of <see cref="PriceAdjustment"/> for product to increase price by percent. 
        /// </summary>
        /// <param name="product">Product for which to adjust price.</param>
        /// <param name="percent">Value in percent by which to adjust price.</param>
        /// <returns>New instance of <see cref="PriceAdjustment"/>.</returns>
        public static PriceAdjustment IncreasePercent(Product product, int percent)
        {
            return NewWithParameter(AsProductValue(product, AsIncreaseValue(AsPercentValue(percent))));
        }

        /// <summary>
        /// Initializes new instance of <see cref="PriceAdjustment"/> to decrease price by amount of NOK. 
        /// </summary>
        /// <param name="amount">Value in NOK by which to adjust price.</param>
        /// <returns>New instance of <see cref="PriceAdjustment"/>.</returns>
        public static PriceAdjustment Decrease(int amount)
        {
            return NewWithParameter(AsDecreaseValue(amount));
        }

        /// <summary>
        /// Initializes new instance of <see cref="PriceAdjustment"/> for product to decrease price by amount of NOK.
        /// </summary>
        /// <param name="product">Product for which to adjust price.</param>
        /// <param name="amount">Value in NOK by which to adjust price.</param>
        /// <returns>New instance of <see cref="PriceAdjustment"/>.</returns>
        public static PriceAdjustment Decrease(Product product, int amount)
        {
            return NewWithParameter(AsProductValue(product, AsDecreaseValue(amount)));
        }

        /// <summary>
        /// Initializes new instance of <see cref="PriceAdjustment"/> to decrease price by percent.
        /// </summary>
        /// <param name="percent">Value in percent by which to adjust price.</param>
        /// <returns>New instance of <see cref="PriceAdjustment"/>.</returns>
        public static PriceAdjustment DecreasePercent(int percent)
        {
            return NewWithParameter(AsDecreaseValue(AsPercentValue(percent)));
        }

        /// <summary>
        /// Initializes new instance of <see cref="PriceAdjustment"/> for product to decrease price by percent.
        /// </summary>
        /// <param name="product">Product for which to adjust price.</param>
        /// <param name="percent">Value in percent by which to adjust price.</param>
        /// <returns>New instance of <see cref="PriceAdjustment"/>.</returns>
        public static PriceAdjustment DecreasePercent(Product product, int percent)
        {
            return NewWithParameter(AsProductValue(product, AsDecreaseValue(AsPercentValue(percent))));
        }

        private static PriceAdjustment NewWithParameter(string parameterValue)
        {
            return new PriceAdjustment
            {
                Items = new NameValueCollection
                {
                    {ParameterName, parameterValue}
                }
            };
        }

        private static string AsIncreaseValue(object value)
        {
            return string.Format(IncreaseFormat, value);
        }

        private static string AsDecreaseValue(object value)
        {
            return string.Format(DecreaseFormat, value);
        }

        private static string AsProductValue(Product product, object value)
        {
            return string.Format(ProductFormat, product.Code, value);
        }

        private static string AsPercentValue(object value)
        {
            return string.Format(PercentFormat, value);
        }
    }
}