using System;

namespace Geta.Bring.Booking.Model
{
    /// <summary>
    /// Bring Product information.
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Initializes new instance of <see cref="Product"/>.
        /// </summary>
        /// <param name="id">Bring Product code from: http://developer.bring.com/additionalresources/productlist.html?from=shipping .</param>
        /// <param name="customerNumber">Customer number for Bring Product.</param>
        public Product(string id, string customerNumber)
        {
            Services = new Services();
        }

        /// <summary>
        /// Bring Product code from: http://developer.bring.com/additionalresources/productlist.html?from=shipping .
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// Customer number for Bring Product.
        /// </summary>
        public string CustomerNumber { get; }

        /// <summary>
        /// The recipient notification property must be set for all products that allow electronic notification,
        /// in order for the recipient to receive electronic notification.
        /// </summary>
        public Services Services { get; }
    }
}