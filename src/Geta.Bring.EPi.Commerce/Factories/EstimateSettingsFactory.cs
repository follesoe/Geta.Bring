using System.Linq;
using Geta.Bring.EPi.Commerce.Extensions;
using Geta.Bring.EPi.Commerce.Model;
using Geta.Bring.Shipping.Model;
using Geta.Bring.Shipping.Model.QueryParameters;
using Mediachase.Commerce.Orders.Dto;

namespace Geta.Bring.EPi.Commerce.Factories
{
    public class EstimateSettingsFactory : IEstimateSettingsFactory
    {
        public IEstimateSettings CreateFrom(ShippingMethodDto shippingMethod)
        {
            var productId = shippingMethod.GetShippingMethodParameterValue(BringShippingGateway.ParameterNames.BringProductId, null);
            var customerNumber = shippingMethod.GetShippingMethodParameterValue(BringShippingGateway.ParameterNames.BringCustomerNumber, null);

            var postalCodeFrom = shippingMethod.GetShippingMethodParameterValue(BringShippingGateway.ParameterNames.PostalCodeFrom, null)
                                 ?? shippingMethod.GetShippingOptionParameterValue(BringShippingGateway.ParameterNames.PostalCodeFrom);

            var countryCodeFrom = shippingMethod.GetShippingOptionParameterValue(BringShippingGateway.ParameterNames.CountryFrom, "NOR")
                                                .ToIso2CountryCode();

            var ediParameter = shippingMethod.GetShippingMethodParameterValue(BringShippingGateway.ParameterNames.Edi, "true");
            bool.TryParse(ediParameter, out var edi);

            var postingAtPostOfficeParameter = shippingMethod.GetShippingMethodParameterValue(BringShippingGateway.ParameterNames.PostingAtPostOffice, "false");
            bool.TryParse(postingAtPostOfficeParameter, out var postingAtPostOffice);
            
            var additionalServicesCodes = shippingMethod.GetShippingMethodParameterValue(BringShippingGateway.ParameterNames.AdditionalServices);
            var services = additionalServicesCodes.Split(',')
                .Select(code => AdditionalService.All.FirstOrDefault(x => x.Code == code))
                .Where(service => service != null);

            var priceExclTaxParameter = shippingMethod.GetShippingMethodParameterValue(BringShippingGateway.ParameterNames.PriceExclTax);
            bool.TryParse(priceExclTaxParameter, out var priceExclTax);

            var priceRoundingParameterParameter = shippingMethod.GetShippingMethodParameterValue(BringShippingGateway.ParameterNames.PriceRounding, "false");
            bool.TryParse(priceRoundingParameterParameter, out bool priceRounding);

            var priceAdjustmentPercentParameter = shippingMethod.GetShippingMethodParameterValue(BringShippingGateway.ParameterNames.PriceAdjustmentPercent, "0");
            int.TryParse(priceAdjustmentPercentParameter, out var priceAdjustmentPercent);

            var priceAdjustmentOperatorParameter = shippingMethod.GetShippingMethodParameterValue(BringShippingGateway.ParameterNames.PriceAdjustmentOperator, "true");
            bool.TryParse(priceAdjustmentOperatorParameter, out var priceAdjustmentAdd);

            return new BringEstimateSettings
            {
                BringProductId = productId,
                BringCustomerNumber = customerNumber,
                PostalCodeFrom = postalCodeFrom,
                CountryCodeFrom = countryCodeFrom,
                Edi = edi,
                PostingAtPostOffice = postingAtPostOffice,
                PriceExclTax = priceExclTax,
                PriceAdjustmentIsAddition = priceAdjustmentAdd,
                PriceAdjustmentPercent = priceAdjustmentPercent,
                PriceRounding = priceRounding,
                AdditionalServices = services
            };
        }
    }
}