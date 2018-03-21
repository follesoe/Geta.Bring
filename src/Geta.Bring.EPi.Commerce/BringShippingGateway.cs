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

namespace Geta.Bring.EPi.Commerce
{
    public class BringShippingGateway : IShippingGateway
    {
        private readonly IShippingClient _shippingClient;

        public BringShippingGateway()
        {
            _shippingClient = ServiceLocator.Current.GetInstance<IShippingClient>();
        }

        // ReSharper disable once UnusedParameter.Local
        public BringShippingGateway(IMarket market)
            : this()
        {
        }

        public ShippingRate GetRate(Guid methodId, Shipment shipment, ref string message)
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

            var shipmentLineItems = Shipment.GetShipmentLineItems(shipment);
            if (shipmentLineItems.Count == 0)
            {
                message = ErrorMessages.ShipmentContainsNoLineItems;
                return null;
            }

            if (shipment.Parent == null || shipment.Parent.Parent == null)
            {
                message = ErrorMessages.OrderFormOrOrderGroupNotFound;
                return null;
            }

            var orderAddress =
                shipment.Parent
                    .Parent
                    .OrderAddresses
                    .FirstOrDefault(address => address.Name == shipment.ShippingAddressId);
            if (orderAddress == null)
            {
                message = ErrorMessages.ShipmentAddressNotFound;
                return null;
            }

            var query = BuildQuery(orderAddress, shippingMethod, shipment, shipmentLineItems);
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

        private static EstimateQuery BuildQuery(
            OrderAddress orderAddress,
            ShippingMethodDto shippingMethod,
            Shipment shipment,
            IEnumerable<LineItem> shipmentLineItems)
        {
            var shipmentLeg = CreateShipmentLeg(orderAddress, shippingMethod);
            var packageSize = CreatePackageSize(shipment, shipmentLineItems);
            var additionalParameters = CreateAdditionalParameters(shippingMethod);
            return new EstimateQuery(
                shipmentLeg,
                packageSize,
                additionalParameters.ToArray());
        }

        private static IEnumerable<IShippingQueryParameter> CreateAdditionalParameters(ShippingMethodDto shippingMethod)
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

        private static PackageSize CreatePackageSize(Shipment shipment, IEnumerable<LineItem> shipmentLineItems)
        {
            var weight = shipmentLineItems
                .Select(item => item.GetWeight() * Shipment.GetLineItemQuantity(shipment, item.Id))
                .Sum() * 1000; // KG to grams
            return PackageSize.InGrams((int)weight);
        }

        private static ShipmentLeg CreateShipmentLeg(OrderAddress orderAddress, ShippingMethodDto shippingMethod)
        {
            var postalCodeFrom = shippingMethod.GetShippingMethodParameterValue(ParameterNames.PostalCodeFrom, null)
                                 ?? shippingMethod.GetShippingOptionParameterValue(ParameterNames.PostalCodeFrom);
            var countryCodeFrom = shippingMethod
                .GetShippingOptionParameterValue(ParameterNames.CountryFrom, "NOR")
                .ToIso2CountryCode();
            var countryCodeTo = orderAddress.CountryCode.ToIso2CountryCode();
            return new ShipmentLeg(postalCodeFrom, orderAddress.PostalCode, countryCodeFrom, countryCodeTo);
        }

        private ShippingRate CreateShippingRate(
            Guid methodId,
            ShippingMethodDto shippingMethod,
            EstimateResult<ShipmentEstimate> result)
        {
            var estimate = result.Estimates.First();

            var usesAdditionalServices = !string.IsNullOrEmpty(shippingMethod.GetShippingMethodParameterValue(ParameterNames.AdditionalServices));

            var amount = AdjustPrice(shippingMethod, usesAdditionalServices ? (decimal)estimate.PackagePrice.PackagePriceWithAdditionalServices.AmountWithVAT :
                                                                              (decimal)estimate.PackagePrice.PackagePriceWithoutAdditionalServices.AmountWithVAT);

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