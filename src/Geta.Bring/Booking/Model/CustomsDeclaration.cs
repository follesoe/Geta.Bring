using System;
using System.Collections.Generic;
using Geta.Bring.Booking.Infrastructure;
using Newtonsoft.Json;

namespace Geta.Bring.Booking.Model
{
    public class CustomsDeclarations
    {
        /// <summary>
        /// When included during booking, the customs information will be sent electronically from Bring to the
        /// destination country.
        /// </summary>
        public List<CustomsDeclaration> EdiCustomsDeclaration { get; set; }

        /// <summary>
        /// The nature of the transaction set as enum of type <see cref="Geta.Bring.Booking.Model.NatureOfTransaction" />
        /// </summary>
        [JsonConverter(typeof(NatureOfTransactionConverter))]
        [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
        public NatureOfTransaction? NatureOfTransaction { get; set; }
    }

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
        [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
        public string CustomsArticleNumber { get; set; }

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
        [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
        public string CountryOfOrigin { get; set; }
    }
}