namespace Geta.Bring.Shipping.Model
{
    public class DeliveryEstimateQueryHandler : QueryHandler<DeliveryEstimate>
    {
        public DeliveryEstimateQueryHandler(ShippingSettings settings)
            : base(settings, "expectedDelivery")
        {
        }

        internal override DeliveryEstimate MapProduct(ProductResponse response)
        {
            return new DeliveryEstimate(
                Product.GetByCode(response.Id),
                response.ExpectedDelivery);
        }
    }
}