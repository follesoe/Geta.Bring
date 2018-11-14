using System.Collections.Generic;
using Geta.Bring.Shipping.Model;

namespace Geta.Bring.EPi.Commerce.Model
{
    public interface IEstimateSettings
    {
        string BringProductId { get; }
        string BringCustomerNumber { get; }
        string PostalCodeFrom { get; }
        string CountryCodeFrom { get; }
        bool Edi { get; }
        bool PostingAtPostOffice { get; }
        bool PriceExclTax { get; }
        bool PriceRounding { get; }
        bool PriceAdjustmentIsAddition { get; }
        int PriceAdjustmentPercent { get; }
        IEnumerable<AdditionalService> AdditionalServices { get; }
    }
}