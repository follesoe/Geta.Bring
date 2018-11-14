using System.Collections.Generic;
using Geta.Bring.Shipping.Model;

namespace Geta.Bring.EPi.Commerce.Model
{
    public class BringEstimateSettings : IEstimateSettings
    {
        public string BringProductId { get; set; }
        public string BringCustomerNumber { get; set; }
        public string PostalCodeFrom { get; set; }
        public string CountryCodeFrom { get; set; }
        public bool Edi { get; set; }
        public bool PostingAtPostOffice { get; set; }
        public bool PriceExclTax { get; set; }
        public bool PriceRounding { get; set; }
        public bool PriceAdjustmentIsAddition { get; set; }
        public int PriceAdjustmentPercent { get; set; }
        public IEnumerable<AdditionalService> AdditionalServices { get; set; }

        
    }
}