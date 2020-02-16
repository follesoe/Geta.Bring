using System;
using System.Collections.Generic;
using Geta.Bring.Booking;
using Geta.Bring.Booking.Model;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;
using Microsoft.Extensions.Configuration;

namespace Geta.Bring.Standard.Test
{
    public class BookingMailboxParcelTest
    {
        private readonly ITestOutputHelper output;
        private readonly IConfigurationRoot configRoot;

        public BookingMailboxParcelTest(ITestOutputHelper output)
        {
            this.output = output;
            this.configRoot = TestHelper.GetIConfigurationRoot();
        }

        [Fact]
        public async void it_can_book_mailbox_parcel()
        {
            var clientUri = new Uri("http://test.localtest.me");
            var key = configRoot["key"];
            var uid = configRoot["uid"];
            var customerNumber = configRoot["customerNumber"];
            var customerNumberMain = customerNumber.Replace("PARCELS_NORWAY-", "");

            var bookingSettings = new BookingSettings(uid, key, clientUri, true);
            var bookingClient = new BookingClient(bookingSettings);

            var parties = new Parties(
                new Party(
                    name: "Test Testeson",
                    addressLine: "Address From",
                    addressLine2: null,
                    postalCode: "9700",
                    city: "Lakselv",
                    countryCode: "NO",
                    reference: "senderReference",
                    additionalAddressInfo: null,
                    contact: new Contact("Jonas Follesø", "test@testeson.no", "90012345")),
                new Party(
                    name: "Test Testeson",
                    addressLine: "Address To",
                    addressLine2: null,
                    postalCode: "7043",
                    city: "Trondheim",
                    countryCode: "NO",
                    reference: "recipientReference",
                    additionalAddressInfo: null,
                    contact: new Contact("Jonas Follesø", "test@testeson.no", "90012345"))
                );

            var product = new Product("3584", customerNumberMain);

            var packages = new List<Package>
            {
                new Package("correlationId", 0.2, "Test Package", new Dimensions(10, 10, 2))
            };

            var consignment = new Consignment(
                correlationId: "correlationId",
                shippingDateTime: DateTime.Today.AddDays(5),
                parties: parties,
                product: product,
                packages: packages);

            var result = await bookingClient.BookAsync(consignment);
            result.Should().NotBeNull();
            result.Errors.Should().BeEmpty();
            result.LabelsLink.Should().NotBeNull();

            output.WriteLine($"Labels Link: {result.LabelsLink}");
        }
    }
}
;