using Newtonsoft.Json;

namespace Geta.Bring.Shipping.Model
{
    internal class ProductResponse
    {
        public ProductResponse(
            string productId, 
            string productCodeInProductionSystem, 
            GuiInformation guiInformation, 
            PackagePrice price,
            ExpectedDelivery expectedDelivery)
        {
            ExpectedDelivery = expectedDelivery;
            Price = price;
            GuiInformation = guiInformation;
            ProductCodeInProductionSystem = productCodeInProductionSystem;
            ProductId = productId;
        }

        [JsonConstructor]
        public ProductResponse(
            string productId, 
            string productCodeInProductionSystem, 
            GuiInformation guiInformation, 
            PackagePrice price,
            PackagePrice netPrice,
            ExpectedDelivery expectedDelivery) : 
                this(productId, productCodeInProductionSystem, guiInformation, price, expectedDelivery)
        {
            NetPrice = netPrice;
        }

        public string ProductId { get; }
        public string ProductCodeInProductionSystem { get; }
        public GuiInformation GuiInformation { get; }
        public PackagePrice Price { get; }
        public PackagePrice NetPrice { get; }
        public ExpectedDelivery ExpectedDelivery { get; }
    }
}