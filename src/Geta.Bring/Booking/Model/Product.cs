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
            CustomerNumber = customerNumber ?? throw new ArgumentNullException(nameof(customerNumber));
            Id = id ?? throw new ArgumentNullException(nameof(id));
        }

        /// <summary>
        /// Bring Product code from: http://developer.bring.com/additionalresources/productlist.html?from=shipping .
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// Customer number for Bring Product.
        /// </summary>
        public string CustomerNumber { get; }
    }
}