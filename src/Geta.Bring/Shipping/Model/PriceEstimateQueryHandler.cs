namespace Geta.Bring.Shipping.Model
{
    public class PriceEstimateQueryHandler : QueryHandler<PriceEstimate>
    {
        public PriceEstimateQueryHandler(ShippingSettings settings) 
            : base(settings, "price")
        {
        }

        internal override PriceEstimate MapProduct(ProductResponse response)
        {
            return new PriceEstimate(
                Product.GetByCode(response.ProductId),
                response.Price);
        }
    }
}