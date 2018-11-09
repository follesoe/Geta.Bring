using System;
using System.Collections.Specialized;

namespace Geta.Bring.Shipping.Model.QueryParameters
{
    /// <summary>
    /// Query parameter to describe package shipping date and/or time to Bring.
    /// </summary>
    public class ShippingDateAndTime : IShippingQueryParameter
    {
        /// <summary>
        /// Initializes new instance of <see cref="ShippingDateAndTime"/> with date.
        /// </summary>
        /// <param name="date">Date of delivery to Bring.</param>
        public ShippingDateAndTime(DateTime date)
        {
            Items = new NameValueCollection
            {
                {"date", date.ToString("yyyy-MM-dd")}
            };
        }

        /// <summary>
        /// Initializes new instance of <see cref="ShippingDateAndTime"/> with date and time. 
        /// </summary>
        /// <param name="date">Date of delivery to Bring.</param>
        /// <param name="time">Time of delivery to Bring.</param>
        public ShippingDateAndTime(DateTime date, TimeSpan time)
        {
            Items = new NameValueCollection
            {
                {"date", date.ToString("yyyy-MM-dd")},
                {"time", time.ToString("HH:mm")}
            };
        }

        public NameValueCollection Items { get; }
    }
}