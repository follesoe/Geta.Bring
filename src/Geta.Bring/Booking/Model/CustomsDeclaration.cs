using System;

namespace Geta.Bring.Booking.Model
{
    /// <summary>
    /// A customs declaration line used to represent customs information for a
    /// a single item in the booking. 
    /// https://developer.bring.com/api/booking/#customs-information
    /// </summary>
    public class CustomsDeclaration
    {
        /// <summary>
        /// Initializes new instance of <see cref="CustomsDeclaration"/>.
        /// </summary>
        public CustomsDeclaration(int quantity, string description, decimal itemNetWeightInKg, int tarriffLineAmount, string currency)
        {
            Quantity = quantity;
            GoodsDescription = description ?? throw new ArgumentNullException(nameof(description));
            ItemNetWeightInKg = itemNetWeightInKg;
            TarriffLineAmount = tarriffLineAmount;
            Currency = currency ?? throw new ArgumentNullException(nameof(currency));
        }

        /// <summary>
        /// Number of items.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Description of contents.
        /// </summary>
        public string GoodsDescription { get; set; }

        /// <summary>
        /// Customs tariff codes (tolltariffnummer, optional). 
        /// Can be found here: https://tolltariffen.toll.no/templates_TAD/Tolltariffen/StartPage.aspx?id=358571
        /// </summary>
        public string customsArticleNumber { get; set; }

        /// <summary>
        /// Total content net weight (kg).
        /// </summary>
        public decimal ItemNetWeightInKg { get; set; }

        /// <summary>
        /// Total content value.
        /// </summary>
        public int TarriffLineAmount { get; set; }

        /// <summary>
        /// The currency of the <see cref="TarriffLineAmount"/>
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// Country of origin for the item (optional).
        /// </summary>
        public string CountryOfOrigin { get; set; }
    }
}