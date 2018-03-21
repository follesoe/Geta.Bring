//using System;
//using FluentAssertions;
//using Geta.Bring.Shipping.Model;
//using Newtonsoft.Json;
//using Xunit;
//
//namespace Tests.Integration.Shipping
//{
//    public class SingleProductResponseDeserializationTests
//    {
//        [Fact]
//        public void it_can_be_deserialized()
//        {
//            var expected = new ShippingResponse(
//                new[]
//                {
//                    new ProductResponse(
//                        "SERVICEPAKKE",
//                        "1202",
//                        new GuiInformation(
//                            11,
//                            "Hent varene selv",
//                            null,
//                            "På posten",
//                            "Klimanøytral Servicepakke",
//                            "Rimi Vinterbro. Åpningstider Man - Fre: 1000-2100, Lør: 0900-1800",
//                            "Sendingen er en Klimanøytral Servicepakke som blir levert til mottakers postkontor/ post i butikk. Mottaker kan velge å hente sendingen på et annet postkontor/post i butikk enn sitt lokale. Mottaker varsles om at sendingen er ankommet via SMS, e-post eller hentemelding i postkassen. Transporttid er normalt 1-3 virkedager, avhengig av strekning. Sendingen kan spores ved hjelp av sporingsnummeret.",
//                            "Billigst!",
//                            35
//                            ),
//                        new PackagePrice(
//                            "NOK",
//                            new Price(126.0, 31.5, 157.5),
//                            new Price(126.0, 31.5, 157.5)
//                            ),
//                        new ExpectedDelivery(
//                            "1",
//                            null,
//                            "28.11.2014",
//                            null,
//                            new DateTime(2014, 11, 28),
//                            null
//                            )
//                        )
//                },
//                new TraceMessages(
//                    new[]
//                    {
//                        "Added fee 'brev-varsling' (NOK 7.00) to base price of SERVICEPAKKE since request did not have additional service 'eVarsling' specified.",
//                        "Package exceed maximum measurements for product B-POST ",
//                        "Package exceed maximum measurements for product A-POST "
//                    })
//                );
//
//            var actual = JsonConvert.DeserializeObject<ShippingResponse>(SingleProductSuccessJsonResponse);
//
//            expected.ShouldBeEquivalentTo(actual);
//        }
//
//        private const string SingleProductSuccessJsonResponse = @"
//{
//    ""@packageId"": ""0"",
//    ""Product"": {
//            ""ProductId"": ""SERVICEPAKKE"",
//            ""ProductCodeInProductionSystem"": ""1202"",
//            ""GuiInformation"": {
//                ""SortOrder"": ""11"",
//                ""MainDisplayCategory"": ""Hent varene selv"",
//                ""SubDisplayCategory"": null,
//                ""DisplayName"": ""På posten"",
//                ""ProductName"": ""Klimanøytral Servicepakke"",
//                ""DescriptionText"": ""Rimi Vinterbro. Åpningstider Man - Fre: 1000-2100, Lør: 0900-1800"",
//                ""HelpText"": ""Sendingen er en Klimanøytral Servicepakke som blir levert til mottakers postkontor/ post i butikk. Mottaker kan velge å hente sendingen på et annet postkontor/post i butikk enn sitt lokale. Mottaker varsles om at sendingen er ankommet via SMS, e-post eller hentemelding i postkassen. Transporttid er normalt 1-3 virkedager, avhengig av strekning. Sendingen kan spores ved hjelp av sporingsnummeret."",
//                ""Tip"": ""Billigst!"",
//                ""MaxWeightInKgs"": ""35""
//            },
//            ""Price"": {
//                ""@currencyIdentificationCode"": ""NOK"",
//                ""PackagePriceWithoutAdditionalServices"": {
//                    ""AmountWithoutVAT"": ""126.00"",
//                    ""VAT"": ""31.50"",
//                    ""AmountWithVAT"": ""157.50""
//                },
//                ""PackagePriceWithAdditionalServices"": {
//                    ""AmountWithoutVAT"": ""126.00"",
//                    ""VAT"": ""31.50"",
//                    ""AmountWithVAT"": ""157.50""
//                }
//            },
//            ""ExpectedDelivery"": {
//                ""WorkingDays"": ""1"",
//                ""UserMessage"": null,
//                ""FormattedExpectedDeliveryDate"": ""28.11.2014"",
//                ""ExpectedDeliveryDate"": {
//                    ""Year"": ""2014"",
//                    ""Month"": ""11"",
//                    ""Day"": ""28""
//                },
//                ""AlternativeDeliveryDates"": null
//            }
//        },
//    ""TraceMessages"": {
//        ""Message"": [
//            ""Added fee 'brev-varsling' (NOK 7.00) to base price of SERVICEPAKKE since request did not have additional service 'eVarsling' specified."",
//            ""Package exceed maximum measurements for product B-POST "",
//            ""Package exceed maximum measurements for product A-POST ""
//        ]
//    }
//}
//"; 
//    }
//}