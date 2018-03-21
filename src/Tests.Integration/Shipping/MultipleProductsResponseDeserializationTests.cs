//using System;
//using FluentAssertions;
//using Geta.Bring.Shipping.Model;
//using Newtonsoft.Json;
//using Xunit;
//
//namespace Tests.Integration.Shipping
//{
//    public class MultipleProductsResponseDeserializationTests
//    {
////        [Fact]
////        
////        public void it_can_be_deserialized()
////        {
////            var expected = new ShippingResponse(
////                new []
////                {
////                    new ProductResponse(
////                        "SERVICEPAKKE",
////                        "1202",
////                        new GuiInformation(
////                            11,
////                            "Hent varene selv",
////                            null,
////                            "På posten",
////                            "Klimanøytral Servicepakke",
////                            "Rimi Vinterbro. Åpningstider Man - Fre: 1000-2100, Lør: 0900-1800",
////                            "Sendingen er en Klimanøytral Servicepakke som blir levert til mottakers postkontor/ post i butikk. Mottaker kan velge å hente sendingen på et annet postkontor/post i butikk enn sitt lokale. Mottaker varsles om at sendingen er ankommet via SMS, e-post eller hentemelding i postkassen. Transporttid er normalt 1-3 virkedager, avhengig av strekning. Sendingen kan spores ved hjelp av sporingsnummeret.",
////                            "Billigst!",
////                            35
////                            ),
////                        new PackagePrice(
////                            "NOK",
////                            new Price(126.0, 31.5, 157.5),
////                            new Price(126.0, 31.5, 157.5)
////                            ),
////                        new ExpectedDelivery(
////                            "1",
////                            null,
////                            "28.11.2014",
////                            null,
////                            new DateTime(2014, 11, 28),
////                            null
////                            )
////                        ),
////                    new ProductResponse(
////                        "PA_DOREN",
////                        "1736",
////                        new GuiInformation(
////                            41,
////                            "Få varene levert",
////                            "Til døren",
////                            "Hjem på kvelden, 17-21",
////                            "På Døren",
////                            "Varsel på sms. Sjåføren ringer 30 - 60 min. før",
////                            "Sendingen er en På Døren- sending som leveres hjem til  mottaker mellom klokken 17 og 21. Mottaker varsles når sendingen er lastet på bil for utkjøring, via SMS og/eller e-post. Mottaker varsles også på  mobiltelefon 30 - 60 minutter før levering. Dersom sendingen ikke kan leveres,  blir den fraktet til lokalt postkontor/ post i butikk. Mottaker varsles om dette via  SMS, e-post eller hentemelding i postkassen. Sendingen kan spores ved hjelp av sporingsnummeret.",
////                            null,
////                            35
////                            ),
////                        new PackagePrice(
////                            "NOK",
////                            new Price(142.0, 35.5, 177.5),
////                            new Price(142.0, 35.5, 177.5)
////                            ),
////                        new ExpectedDelivery(
////                            "1",
////                            null,
////                            "28.11.2014",
////                            null,
////                            new DateTime(2014, 11, 28),
////                            null
////                            )
////                        )
////                },
////                new TraceMessages(
////                    new[]
////                    {
////                        "Package exceed maximum measurements for product A-POST "
////                    }));
////
////            var actual = JsonConvert.DeserializeObject<ShippingResponse>(MultipleProductsSuccessJsonResponse);
////
////            expected.ShouldBeEquivalentTo(actual);
////        }
//
//        private const string MultipleProductsSuccessJsonResponse = @"
//{
//    ""@packageId"": ""0"",
//    ""Product"": [
//        {
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
//        {
//            ""ProductId"": ""PA_DOREN"",
//            ""ProductCodeInProductionSystem"": ""1736"",
//            ""GuiInformation"": {
//                ""SortOrder"": ""41"",
//                ""MainDisplayCategory"": ""Få varene levert"",
//                ""SubDisplayCategory"": ""Til døren"",
//                ""DisplayName"": ""Hjem på kvelden, 17-21"",
//                ""ProductName"": ""På Døren"",
//                ""DescriptionText"": ""Varsel på sms. Sjåføren ringer 30 - 60 min. før"",
//                ""HelpText"": ""Sendingen er en På Døren- sending som leveres hjem til  mottaker mellom klokken 17 og 21. Mottaker varsles når sendingen er lastet på bil for utkjøring, via SMS og/eller e-post. Mottaker varsles også på  mobiltelefon 30 - 60 minutter før levering. Dersom sendingen ikke kan leveres,  blir den fraktet til lokalt postkontor/ post i butikk. Mottaker varsles om dette via  SMS, e-post eller hentemelding i postkassen. Sendingen kan spores ved hjelp av sporingsnummeret."",
//                ""Tip"": null,
//                ""MaxWeightInKgs"": ""35""
//            },
//            ""Price"": {
//                ""@currencyIdentificationCode"": ""NOK"",
//                ""PackagePriceWithoutAdditionalServices"": {
//                    ""AmountWithoutVAT"": ""142.00"",
//                    ""VAT"": ""35.50"",
//                    ""AmountWithVAT"": ""177.50""
//                },
//                ""PackagePriceWithAdditionalServices"": {
//                    ""AmountWithoutVAT"": ""142.00"",
//                    ""VAT"": ""35.50"",
//                    ""AmountWithVAT"": ""177.50""
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
//        }
//    ],
//    ""TraceMessages"": {
//        ""Message"": ""Package exceed maximum measurements for product A-POST ""
//    }
//}
//";
//    }
//}