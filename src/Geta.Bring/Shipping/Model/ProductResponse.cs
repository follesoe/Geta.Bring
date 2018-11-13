using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Geta.Bring.Shipping.Infrastructure;
using Geta.Bring.Shipping.Model.Errors;
using System.Linq;

namespace Geta.Bring.Shipping.Model
{
    internal class ProductResponse
    {
        public ProductResponse(
            string id, 
            string productionCode)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            ProductionCode = productionCode ?? throw new ArgumentNullException(nameof(productionCode));
        }

        public ProductResponse(string id, string productionCode,
            GuiInformation guiInformation,
            PackagePrices price,
            ExpectedDelivery expectedDelivery) 
            : this(id, productionCode)
        {   
            GuiInformation = guiInformation;
            Price = price;
            ExpectedDelivery = expectedDelivery;
        }

        [JsonConstructor]
        public ProductResponse(string id, string productionCode,
            GuiInformation guiInformation,
            PackagePrices price,
            ExpectedDelivery expectedDelivery,
            IEnumerable<ProductError> errors) 
            : this(id, productionCode, guiInformation, price, expectedDelivery)
        {
            Errors = errors ?? Enumerable.Empty<ProductError>();
        }
        
        public string Id { get; }
        public string ProductionCode { get; }

        public PackagePrices Price { get; }
        public GuiInformation GuiInformation { get; }
        public ExpectedDelivery ExpectedDelivery { get; }

        [JsonConverter(typeof(ObjectToArrayConverter<ProductError>))]
        public IEnumerable<ProductError> Errors { get; }
    }
}