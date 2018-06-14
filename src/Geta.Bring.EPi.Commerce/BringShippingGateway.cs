using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EPiServer.ServiceLocation;
using Geta.Bring.EPi.Commerce.Extensions;
using Geta.Bring.EPi.Commerce.Model;
using Geta.Bring.Shipping.Model;
using Geta.Bring.Shipping.Model.QueryParameters;
using Mediachase.Commerce;
using Mediachase.Commerce.Orders;
using Mediachase.Commerce.Orders.Dto;
using Mediachase.Commerce.Orders.Managers;
using Geta.Bring.Shipping;
using EPiServer.Commerce.Order;
using Mediachase.Commerce.Inventory;

namespace Geta.Bring.EPi.Commerce
{
    public class BringShippingGateway : IShippingPlugin
    {
        private readonly IShippingClient _shippingClient;
        private readonly IWarehouseRepository _warehouseRepository;

        public BringShippingGateway(IShippingClient shippingClient, IWarehouseRepository warehouseRepository)
        {
            _shippingClient = shippingClient;
            _warehouseRepository = warehouseRepository;
        }

        public ShippingRate GetRate(IMarket market, Guid methodId, IShipment shipment, ref string message)
        {
            return GetRate(methodId, shipment, ref message);
        }

        public ShippingRate GetRate(Guid methodId, IShipment shipment, ref string message)
        {
            if (shipment == null)
            {
                return null;
            }

            var shippingMethod = ShippingManager.GetShippingMethod(methodId);
            if (shippingMethod == null || shippingMethod.ShippingMethod.Count <= 0)
            {
                message = string.Format(ErrorMessages.ShippingMethodCouldNotBeLoaded, methodId);
                return null;
            }

            var shipmentLineItems = shipment.LineItems;
            if (shipmentLineItems.Count == 0)
            {
                message = ErrorMessages.ShipmentContainsNoLineItems;
                return null;
            }

            if (shipment.ShippingAddress == null)
            {
                message = ErrorMessages.ShipmentAddressNotFound;
                return null;
            }

            var query = BuildQuery(shipment, shippingMethod, shipmentLineItems);
            var estimate = _shippingClient.FindAsync<ShipmentEstimate>(query).Result;
            if (estimate.Success && estimate.Estimates.Any())
            {
                return CreateShippingRate(methodId, shippingMethod, estimate);
            }

            message = estimate.ErrorMessages
                .Aggregate(new StringBuilder(), (sb, msg) =>
                {
                    sb.Append(msg);
                    sb.AppendLine();
                    return sb;
                }).ToString();
            return null;
        }

        private EstimateQuery BuildQuery(
            IShipment shipment,
            ShippingMethodDto shippingMethod,
            IEnumerable<ILineItem> shipmentLineItems)
        {
            var shipmentLeg = CreateShipmentLeg(shipment, shippingMethod);
            var packageSize = CreatePackageSize(shipmentLineItems);
            var additionalParameters = CreateAdditionalParameters(shippingMethod);
            return new EstimateQuery(
                shipmentLeg,
                packageSize,
                additionalParameters.ToArray());
        }

        private IEnumerable<IShippingQueryParameter> CreateAdditionalParameters(ShippingMethodDto shippingMethod)
        {
            var hasEdi = bool.Parse(shippingMethod.GetShippingMethodParameterValue(ParameterNames.Edi, "true"));
            yield return new Edi(hasEdi);

            var shippedFromPostOffice =
                bool.Parse(shippingMethod.GetShippingMethodParameterValue(ParameterNames.PostingAtPostOffice, "false"));
            yield return new ShippedFromPostOffice(shippedFromPostOffice);

            int priceAdjustmentPercent;
            int.TryParse(shippingMethod.GetShippingMethodParameterValue(ParameterNames.PriceAdjustmentPercent, "0"), out priceAdjustmentPercent);

            if (priceAdjustmentPercent > 0)
            {
                bool priceAdjustmentAdd;
                bool.TryParse(shippingMethod.GetShippingMethodParameterValue(ParameterNames.PriceAdjustmentOperator, "true"), out priceAdjustmentAdd);

                yield return priceAdjustmentAdd ? PriceAdjustment.IncreasePercent(priceAdjustmentPercent) : PriceAdjustment.DecreasePercent(priceAdjustmentPercent);
            }

            var productCode = shippingMethod.GetShippingMethodParameterValue(ParameterNames.BringProductId, null)
                              ?? Product.Servicepakke.Code;
            yield return new Products(Product.GetByCode(productCode));

            var customerNumber = shippingMethod.GetShippingMethodParameterValue(ParameterNames.BringCustomerNumber, null);
            if (!string.IsNullOrWhiteSpace(customerNumber))
            {
                yield return new CustomerNumber(customerNumber);
            }

            var additionalServicesCodes = shippingMethod.GetShippingMethodParameterValue(ParameterNames.AdditionalServices);
            var services = additionalServicesCodes.Split(',')
                .Select(code => AdditionalService.All.FirstOrDefault(x => x.Code == code))
                .Where(service => service != null);
            yield return new AdditionalServices(services.ToArray());
        }

        private PackageSize CreatePackageSize(IEnumerable<ILineItem> shipmentLineItems)
        {
            var weight = shipmentLineItems
                .Select(item => item.GetWeight() * item.Quantity)
                .Sum() * 1000; // KG to grams

            return PackageSize.InGrams((int)weight);
        }

        private ShipmentLeg CreateShipmentLeg(IShipment shipment, ShippingMethodDto shippingMethod)
        {
            var postalCodeFrom = shippingMethod.GetShippingMethodParameterValue(ParameterNames.PostalCodeFrom, null)
                                 ?? shippingMethod.GetShippingOptionParameterValue(ParameterNames.PostalCodeFrom);

            var countryCodeFrom = shippingMethod
                .GetShippingOptionParameterValue(ParameterNames.CountryFrom, "NOR")
                .ToIso2CountryCode();

            if (string.IsNullOrEmpty(shipment.WarehouseCode) == false)
            {
                var warehouse = _warehouseRepository.Get(shipment.WarehouseCode);
                var warehousePostalCode = warehouse.ContactInformation?.PostalCode;
                var warehouseCountryCode = warehouse.ContactInformation?.CountryCode;

                if (string.IsNullOrEmpty(warehousePostalCode) == false && warehouse.IsPickupLocation)
                {
                    postalCodeFrom = warehousePostalCode;
                    countryCodeFrom = warehouseCountryCode.ToIso2CountryCode();
                }
            }

            var countryCodeTo = shipment.ShippingAddress.CountryCode.ToIso2CountryCode();

            return new ShipmentLeg(postalCodeFrom, shipment.ShippingAddress.PostalCode, countryCodeFrom, countryCodeTo);
        }

        private ShippingRate CreateShippingRate(
            Guid methodId,
            ShippingMethodDto shippingMethod,
            EstimateResult<ShipmentEstimate> result)
        {
            var estimate = result.Estimates.First();
            var priceExclTax = shippingMethod.GetShippingMethodParameterValue(ParameterNames.PriceExclTax) == "True";
            var usesAdditionalServices = !string.IsNullOrEmpty(shippingMethod.GetShippingMethodParameterValue(ParameterNames.AdditionalServices));
            var priceWithAdditionalServices = !priceExclTax
                ? (decimal) estimate.PackagePrice.PackagePriceWithAdditionalServices.AmountWithVAT
                : (decimal) estimate.PackagePrice.PackagePriceWithAdditionalServices.AmountWithoutVAT;
            var priceWithoutAdditionalServices = !priceExclTax
                ? (decimal) estimate.PackagePrice.PackagePriceWithoutAdditionalServices.AmountWithVAT
                : (decimal) estimate.PackagePrice.PackagePriceWithoutAdditionalServices.AmountWithoutVAT;

            var amount = AdjustPrice(shippingMethod, usesAdditionalServices ? priceWithAdditionalServices :
                                                                              priceWithoutAdditionalServices);

            var moneyAmount = new Money(
                amount,
                new Currency(estimate.PackagePrice.CurrencyIdentificationCode));

            return new BringShippingRate(
                methodId,
                estimate.GuiInformation.DisplayName,
                estimate.GuiInformation.MainDisplayCategory,
                estimate.GuiInformation.SubDisplayCategory,
                estimate.GuiInformation.DescriptionText,
                estimate.GuiInformation.HelpText,
                estimate.GuiInformation.Tip,
                estimate.ExpectedDelivery.ExpectedDeliveryDate,
                moneyAmount);
        }

        private decimal AdjustPrice(ShippingMethodDto shippingMethod, decimal price)
        {
            var shippingMethodRow = shippingMethod.ShippingMethod[0];
            var amount = shippingMethodRow.BasePrice + price;
            bool priceRounding;
            if (bool.TryParse(shippingMethod.GetShippingMethodParameterValue(ParameterNames.PriceRounding, "false"), out priceRounding)
                && priceRounding)
            {
                return Math.Round(amount, MidpointRounding.AwayFromZero);
            }
            return amount;
        }

        internal static class ParameterNames
        {
            public const string BringProductId = "BringProductId";
            public const string PostingAtPostOffice = "PostingAtPostOffice";
            public const string PostalCodeFrom = "PostalCodeFrom";
            public const string CountryFrom = "CountryFrom";
            public const string Edi = "EDI";
            public const string AdditionalServices = "AdditionalServices";
            public const string PriceRounding = "PriceRounding";
            public const string PriceAdjustmentOperator = "PriceAdjustmentOperator";
            public const string PriceAdjustmentPercent = "PriceAdjustmentPercent";
            public const string BringCustomerNumber = "BringCustomerNumber";
            public const string PriceExclTax = "PriceExclTax";
        }

        internal static class ErrorMessages
        {
            public const string ShipmentAddressNotFound =
                "The shipment address not found in order group.";

            public const string OrderFormOrOrderGroupNotFound =
                "The order form or order group not found for shipment.";

            public const string ShippingMethodCouldNotBeLoaded =
                "The shipping method could not be loaded by '{0}' id.";

            public const string ShipmentContainsNoLineItems =
                "The shipment doesn't contain any line items.";
        }
    }
}