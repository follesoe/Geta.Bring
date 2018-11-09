using System;
using System.Collections.Generic;
using System.Linq;
using EPiServer.Commerce.Order;
using Geta.Bring.EPi.Commerce.Extensions;
using Geta.Bring.Shipping.Model;
using Geta.Bring.Shipping.Model.QueryParameters;
using Mediachase.Commerce.Inventory;
using Mediachase.Commerce.Orders.Dto;
using Mediachase.Commerce.Orders.Managers;

namespace Geta.Bring.EPi.Commerce.Factories
{
    public class EstimateQueryFactory : IEstimateQueryFactory
    {
        private readonly IWarehouseRepository _warehouseRepository;

        public EstimateQueryFactory(IWarehouseRepository warehouseRepository)
        {
            _warehouseRepository = warehouseRepository;
        }

        public EstimateQuery BuildEstimateQuery(IShipment shipment, Guid shippingMethodId)
        {
            var shipmentLineItems = shipment.LineItems;
            var shippingMethod = ShippingManager.GetShippingMethod(shippingMethodId);
            return BuildQuery(shipment, shippingMethod, shipmentLineItems);
        }

        protected virtual EstimateQuery BuildQuery(
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

        protected virtual ShipmentLeg CreateShipmentLeg(IShipment shipment, ShippingMethodDto shippingMethod)
        {
            var postalCodeFrom = shippingMethod.GetShippingMethodParameterValue(BringShippingGateway.ParameterNames.PostalCodeFrom, null)
                                 ?? shippingMethod.GetShippingOptionParameterValue(BringShippingGateway.ParameterNames.PostalCodeFrom);

            var countryCodeFrom = shippingMethod
                .GetShippingOptionParameterValue(BringShippingGateway.ParameterNames.CountryFrom, "NOR")
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

        protected virtual PackageSize CreatePackageSize(IEnumerable<ILineItem> shipmentLineItems)
        {
            var weight = shipmentLineItems
                             .Select(item => item.GetWeight() * item.Quantity)
                             .Sum() * 1000; // KG to grams

            return PackageSize.InGrams((int)weight);
        }

        protected virtual IEnumerable<IShippingQueryParameter> CreateAdditionalParameters(ShippingMethodDto shippingMethod)
        {
            var hasEdi = bool.Parse(shippingMethod.GetShippingMethodParameterValue(BringShippingGateway.ParameterNames.Edi, "true"));
            yield return new Edi(hasEdi);

            var shippedFromPostOffice =
                bool.Parse(shippingMethod.GetShippingMethodParameterValue(BringShippingGateway.ParameterNames.PostingAtPostOffice, "false"));
            yield return new ShippedFromPostOffice(shippedFromPostOffice);
            
            var productCode = shippingMethod.GetShippingMethodParameterValue(BringShippingGateway.ParameterNames.BringProductId, null)
                              ?? Product.Servicepakke.Code;

            yield return new Products(Product.GetByCode(productCode));

            var additionalServicesCodes = shippingMethod.GetShippingMethodParameterValue(BringShippingGateway.ParameterNames.AdditionalServices);
            var services = additionalServicesCodes.Split(',')
                .Select(code => AdditionalService.All.FirstOrDefault(x => x.Code == code))
                .Where(service => service != null);

            yield return new AdditionalServices(services.ToArray());
        }
    }
}
