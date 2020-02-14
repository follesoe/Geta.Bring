using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using Geta.Bring.Shipping.Infrastructure;
using Geta.Bring.Shipping.Model.Errors;
using System.Linq;

namespace Geta.Bring.Shipping.Model
{
    internal class ProductResponse
    {
        public ProductResponse(string id)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
        }

        public ProductResponse(string id, string productionCode) : this(id)
        {
            ProductionCode = productionCode;
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
            GuiInformation guiInformation = null,
            PackagePrices price = null,
            ExpectedDelivery expectedDelivery = null,
            IEnumerable<ProductError> errors = null)
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