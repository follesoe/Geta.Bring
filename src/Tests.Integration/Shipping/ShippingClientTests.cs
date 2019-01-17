using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Geta.Bring.Shipping;
using Geta.Bring.Shipping.Model;
using Geta.Bring.Shipping.Model.Errors;
using Geta.Bring.Shipping.Model.QueryParameters;
using Xunit;

namespace Tests.Integration.Shipping
{
    public class ShippingClientTests
    {
        [Fact]
        public async Task it_returns_result()
        {
            var clientUri = new Uri("http://test.localtest.me");

            var settings = new ShippingSettings(clientUri, "sven.erik@geta.no", "20b22ed6-2aa7-48c9-9561-b49fe85ce118");
            var sut = new ShippingClient(settings);

            var query = new EstimateQuery(
                new ShipmentLeg("0484", "5600", "NO", "NO"),
                PackageSize.InGrams(2500), 
                new Products(Product.Servicepakke));

            var actual = await sut.FindAsync<ShipmentEstimate>(query).ConfigureAwait(false);

            actual.Estimates.Should().NotBeEmpty();
        }

        [Fact]
        public async Task it_returns_error_when_unauthenticated()
        {
            var clientUri = new Uri("http://test.localtest.me");

            var settings = new ShippingSettings(clientUri, (string)null, null);
            var sut = new ShippingClient(settings);

            var query = new EstimateQuery(
                new ShipmentLeg("0484", "5600", "NO", "NO"),
                PackageSize.InGrams(2500));

            var actual = await sut.FindAsync<ShipmentEstimate>(query).ConfigureAwait(false);

            var responseErrors = actual.Errors.OfType<ResponseError>();

            Assert.Single(responseErrors);
        }

        [Fact]
        public async Task it_returns_error_when_no_product_in_estimate_query()
        {
            var clientUri = new Uri("http://test.localtest.me");

            var settings = new ShippingSettings(clientUri, "sven.erik@geta.no", "20b22ed6-2aa7-48c9-9561-b49fe85ce118");
            var sut = new ShippingClient(settings);

            var query = new EstimateQuery(
                new ShipmentLeg("0484", "5600", "NO", "NO"),
                PackageSize.InGrams(2500));

            var actual = await sut.FindAsync<ShipmentEstimate>(query).ConfigureAwait(false);

            var fieldErrors = actual.Errors.OfType<FieldError>()
                .Where(x => x.Code.Equals("INVALID_ARGUMENT"));

            Assert.Single(fieldErrors);
        }

        [Fact]
        public async Task it_returns_error_because_postal_code_is_wrong()
        {
            var clientUri = new Uri("http://test.localtest.me");

            var settings = new ShippingSettings(clientUri, "sven.erik@geta.no", "20b22ed6-2aa7-48c9-9561-b49fe85ce118");
            var sut = new ShippingClient(settings);

            var query = new EstimateQuery(
                new ShipmentLeg("0000", "5600"),
                PackageSize.InGrams(2500),
                new Products(Product.Servicepakke));

            var actual = await sut.FindAsync<ShipmentEstimate>(query).ConfigureAwait(false);

            var fieldErrors = actual.Errors.OfType<FieldError>()
                                           .Where(x => x.Code.Equals("INVALID_ARGUMENT"));

            Assert.Single(fieldErrors);
        }

        [Fact]
        public async Task it_does_not_return_product_because_weight_is_over_limit()
        {
            var clientUri = new Uri("http://test.localtest.me");

            var settings = new ShippingSettings(clientUri, "sven.erik@geta.no", "20b22ed6-2aa7-48c9-9561-b49fe85ce118");
            var sut = new ShippingClient(settings);

            var query = new EstimateQuery(
                new ShipmentLeg("0151", "5003"),
                PackageSize.InGrams(300000),
                new Products(Product.Servicepakke));

            var actual = await sut.FindAsync<ShipmentEstimate>(query).ConfigureAwait(false);

            var productCount = actual.Estimates.Count();
            var productErrors = actual.Errors.OfType<ProductError>()
                                      .Where(x => x.Code.Equals("INVALID_MEASUREMENTS"));

            Assert.Equal(0, productCount);
            Assert.Single(productErrors);
        }
    }
}