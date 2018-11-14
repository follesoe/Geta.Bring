using System;
using System.Collections.Generic;
using System.Linq;
using EPiServer.Commerce.Order;
using Geta.Bring.EPi.Commerce.Extensions;
using Geta.Bring.EPi.Commerce.Model;
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
        private readonly IEstimateSettingsFactory _estimateSettingsFactory;

        public EstimateQueryFactory(IWarehouseRepository warehouseRepository, IEstimateSettingsFactory estimateSettingsFactory)
        {
            _warehouseRepository = warehouseRepository;
            _estimateSettingsFactory = estimateSettingsFactory;
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
            var settings = _estimateSettingsFactory.CreateFrom(shippingMethod);
            var shipmentLeg = CreateShipmentLeg(shipment, shippingMethod, settings);
            var packageSize = CreatePackageSize(shipmentLineItems);
            var additionalParameters = CreateAdditionalParameters(shippingMethod, settings);

            return new EstimateQuery(
                shipmentLeg,
                packageSize,
                additionalParameters.ToArray());
        }

        protected virtual ShipmentLeg CreateShipmentLeg(IShipment shipment, ShippingMethodDto shippingMethod, IEstimateSettings settings)
        {
            var postalCodeFrom = settings.PostalCodeFrom;
            var countryCodeFrom = settings.CountryCodeFrom;

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

        protected virtual IEnumerable<IShippingQueryParameter> CreateAdditionalParameters(ShippingMethodDto shippingMethod, IEstimateSettings settings)
        {
            yield return new Edi(settings.Edi);
            yield return new ShippedFromPostOffice(settings.PostingAtPostOffice);
            
            var productCode = settings.BringProductId ?? Product.Servicepakke.Code;
            var product = Product.GetByCode(productCode);

            if (!string.IsNullOrWhiteSpace(settings.BringCustomerNumber))
            {
                product.CustomerNumber = settings.BringCustomerNumber;
            }

            yield return new Products(product);
            yield return new AdditionalServices(settings.AdditionalServices.ToArray());
        }
    }
}
