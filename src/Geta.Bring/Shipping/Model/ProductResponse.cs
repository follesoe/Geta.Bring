using Newtonsoft.Json;
using System;

namespace Geta.Bring.Shipping.Model
{
    internal class ProductResponse
    {
        public ProductResponse(
            string productId, 
            string productCodeInProductionSystem)
        {
            ProductId = productId ?? throw new ArgumentNullException(nameof(productId));
            ProductCodeInProductionSystem = productCodeInProductionSystem ?? throw new ArgumentNullException(nameof(productCodeInProductionSystem));
        }

        [JsonConstructor]
        public ProductResponse(string productId,
            string productCodeInProductionSystem,
            PackagePrices price,
            GuiInformation guiInformation,
            ExpectedDelivery expectedDelivery) 
            : this(productId, productCodeInProductionSystem)
        {
            ExpectedDelivery = expectedDelivery;
            GuiInformation = guiInformation;
            Price = price;
        }

        [JsonProperty("id")]
        public string ProductId { get; }

        [JsonProperty("productionCode")]
        public string ProductCodeInProductionSystem { get; }

        public PackagePrices Price { get; }
        public GuiInformation GuiInformation { get; }
        public ExpectedDelivery ExpectedDelivery { get; }
    }
}