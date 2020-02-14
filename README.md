# Bring .NET API and EPiServer Commerce module

![](http://tc.geta.no/app/rest/builds/buildType:(id:TeamFrederik_GetaBring_GetaBringDebug)/statusIcon)

## Description

This is a .NET Bring API library and module to integrate with EPiServer Commerce. Currently it supports these Bring APIs:
- Shipping Guide API 2.0
- Tracking API
- Booking API

## How to install?

Start by installing NuGet packages in you project.

## Package maintainer

https://github.com/svenrog

### Bring API

    Install-Package Geta.Bring

### Upgrading from previous versions (in EPiServer)

Previous versions of this package used Bring Shipping API 1.0. This package is now upgraded to use version 2.0.

Make sure to note that dependency injection now requires that a MyBring user and API key is stated when configuring *IShippingClient*.

    //context is ServiceConfigurationContext from ConfigureContainer() in IConfigurableModule
    var services = context.Services; 
    services.AddTransient<IShippingPlugin, BringShippingGateway>();
    services.AddTransient<IPickupClient>((locator) => new PickupClient(new PickupSettings()));
    services.AddTransient<IShippingClient>((locator) => 
                new ShippingClient(new ShippingSettings(SiteDefinition.Current.SiteUrl ?? new Uri("http://foundation.localtest.me"), 
                ConfigurationManager.AppSettings["Bring:UserId"], 
                ConfigurationManager.AppSettings["Bring:ApiKey"])));
    services.AddTransient<IEstimateQueryFactory, EstimateQueryFactory>();
    services.AddTransient<IEstimateSettingsFactory, EstimateSettingsFactory>();

### EPiServer Commerce module

For EPiServer Commerce you have to install two packages. First package - *Geta.Bring.EPi.Commerce*, should be installed into your EPiServer Commerce Web site:

    Install-Package Geta.Bring.EPi.Commerce

Second package - *Geta.Bring.EPi.Commerce.Manager*, should be installed into your EPiServer Commerce Manager Web site:

    Install-Package Geta.Bring.EPi.Commerce.Manager

## How to use it?

### Shipping Guide API

To start using Shipping Guide API you have to create new *ShippingClient* with provided settings.

    var settings = new ShippingSettings(new Uri("http://test.localtest.me"), "your_uid", "your_key");
    IShippingClient client = new ShippingClient(settings);

*ShippingSettings* requires at least one parameter - *clientUri*, which is the base URI of your Web site.

To find estimated delivery options you have to call *FindAsync* method with query parameters and type of the estimate.
    
     var products = new List<Geta.Bring.Shipping.Model.Product>();
                products.Add(Geta.Bring.Shipping.Model.Product.Servicepakke);
                products.Add(Geta.Bring.Shipping.Model.Product.PaDoren);
                
    var query = new EstimateQuery(
        new ShipmentLeg("0484", "5600"),
        PackageSize.InGrams(2500),
        new Products(products.ToArray()));

    var result = await client.FindAsync<ShipmentEstimate>(query);

There are three types of estimates:
- *DeliveryEstimate* - returns estimated delivery options,
- *PriceEstimate* - returns estimated price options,
- *ShipmentEstimate* - returns estimated delivery, price and GUI information options.

Query requires at least two parameters - *ShipmentLeg*, *PackageSize* parameters and one or more stated *Products*. Additionaly it can have more parameters according to your needs. Full list of parameters:
- *ShipmentLeg* - query parameter to describe shipment source and destination by providing postal codes and/or country codes. Has to contain fromcountry, tocountry, frompostalcode and topostalcode.
- *PackageSize* - query parameter to describe package size in grams, by dimensions or by volume.
- *Products* - query parameter to describe required products. List of available products: https://developer.bring.com/api/products/,
- *AdditionalServices* - query parameter to describe required additional services. List of available services: http://developer.bring.com/additionalresources/productlist.html?from=shipping#additionalServices ,
- *Edi* - query parameter to describe if EDI is used,
- *ShippedFromPostOffice* - query parameter to describe if shipped from post office,
- *ShippingDateAndTime* - query parameter to describe package shipping date and/or time to Bring

### Pickup API

To start using Pickup API you have to create new *PickupClient* with provided settings.

    var settings = new PickupSettings();
    IPickupClient client = new PickupClient(settings);

To find pickup points there are 4 different methods to use.

    client.FindByZipCode(PickupZipCodeQuery query);

Query requires that *CountryCode* and *ZipCode* are set.
- *CountryCode* - Country. Possible values: NO, DK, SE, FI
- *PostalCode* - Postal code.

List of available additional parameters: https://developer.bring.com/api/pickup-point/#pickup-points-for-postal-code

    client.FindByLocation(PickupLocationQuery query);

Query requires that *CountryCode*, *Latitude* and *Longitude* are set.
- *CountryCode* - Country. Possible values: NO, DK, SE, FI
- *Latitude* - Latitude. Geographic coordinate specifying the north-south position.
- *Longitude* - Longitude. Geographic coordinate specifying the east-west position.

List of available additional parameters: https://developer.bring.com/api/pickup-point/#pickup-points-for-location

    client.FindById(string countryCode, string id);

- *countryCode* - Country. Possible values: NO, DK, SE, FI
- *id* - Id of pickup point.

List of available additional parameters: https://developer.bring.com/api/pickup-point/#a-specific-pickup-point

    client.All(string countryCode);

- *countryCode* - Country. Possible values: NO, DK, SE, FI

Full documentation: https://developer.bring.com/api/pickup-point/#all-pickup-points-in-a-country

### Tracking API

To start using Tracking API you have to create new *TrackingClient* with provided settings.

    ITrackingClient client = new TrackingClient();

Default constructor do not require any parameters, but there is available constructor which accepts *TrackingSettings*. *TrackingSettings* by default do not have any paramters, but you can change endpoint URI if it changes. By default it uses http://sporing.bring.no/sporing.json as service endpoint.

To get tracking information you have to call *TrackAsync* method with tracking number.

    var result = await client.TrackAsync("123456789");

The method returns list of package consignment statuses.

### Booking API

To start using Booking API you have to create new *BookingClient* with provided settings.

    var settings = new BookingSettings(
        "Uid",
        "ApiKey",
        new Uri("http://clientUri"),
        test: false
        );
    IBookingClient client = new BookingClient(settings);

*BookingSettings* has four parameters:
- *uid* - Bring API User ID,
- *key* - Bring API Key,
- *clientUri* - base URI of your Web site,
- *isTest* - mark if test mode in use (default: false),
- *endpointUri* - Bring API endpoint (default: https://api.bring.com/booking/api/booking).

To book consignment you have to call *BookAsync* method with consignment details. The overload of the method can handle multiple consignments.

    var sender = new Party(
        "Sender Name",
        "Address 1",
        "Address 2",
        "0123",
        "Oslo",
        "NOR",
        "reference number",
        "additional info",
        new Contact("John", "john@example.com", "98745612"));

    var recipient = new Party(
        "Recipient Name",
        "Address 1",
        "Address 2",
        "0123",
        "Oslo",
        "NOR",
        "reference number",
        "additional info",
        new Contact("Tom", "tom@example.com", "23654789"));

    var product = new Product(
        "A-POST", // Bring product code
        "customer number"); // Customer number for Bring product

    var packages = new[]
    {
        new Package(
            "correlation ID", 
            1.0, // weight in kilograms
            "Products", // goods description
            new Dimensions(10, 10, 10)) // dimensions H W L in cm
    };

    var consignment = new Consignment(
        "correlation ID",
        DateTime.UtcNow.AddDays(1), // date when package delivered to Bring
        new Parties(sender, recipient),
        product,
        packages);

    var result = await sut.BookAsync(consignment);

The method returns confirmation information.

### EPiServer Commerce module

EPiServer Commerce module consists of two libraries: *Geta.Bring.EPi.Commerce* - library which should be installed into your Web site; *Geta.Bring.EPi.Commerce.Manager* - library which should be installed into Commerce Manager Web site. *Geta.Bring.EPi.Commerce* contains shipping gateway - *BringShippingGateway* (implemented as *IShippingPlugin*) and types for shipping details - *BringShippingRate* and *BringShippingRateGroup*. *Geta.Bring.EPi.Commerce.Manager* contains required views to configure EPiServer Commerce Bring module.

#### Getting rates

To get all rates we suggest implementing a ShippingService as in [Episerver Foundation](https://github.com/episerver/Foundation/blob/b29257b2411991407fa84f8166af9d958bdd2f0f/src/Foundation.Commerce/Order/Services/ShippingService.cs): 

Then you can use service to get rates. Example:

    var shipment = cart.GetFirstShipment();
    var methods = _shippingService.GetShippingMethodsByMarket(cart.MarketId.ToString(), false);
    foreach (var shippingMethodInfoModel in methods)
    {
        var rate = _shippingService.GetRate(shipment, shippingMethodInfoModel,
                        currentMarket);
    }

If shipment method is of type BringShipmentGateway, you can cast rate to BringShippingRate to get more info, like estimated delivery date:

    if (rate is BringShippingRate bringShippingRate)
    {
        var estimatedDelivery = bringShippingRate.ExpectedDeliveryDate);    
    }

You can create a partial view to render bring shipping rates. For example:

    @model IEnumerable<Geta.Bring.EPi.Commerce.Model.BringShippingRateGroup>

    @foreach (var shippingGroup in Model)
    {
        <div>
            <h2>@shippingGroup.Name</h2>
            @foreach (var shippingMethod in shippingGroup.ShippingRates)
            {
                <div>
                    <input type="radio" value="@shippingMethod.Id" group="ShippingMethod" />
                    @shippingMethod.Name
                    @shippingMethod.Description
                    @shippingMethod.Money.Amount.ToString("0.00") @shippingMethod.Money.Currency.CurrencyCode
                </div>
            }
        </div>
    }

There is available sample view on [GitHub](https://github.com/Geta/Geta.Bring/tree/master/docs/example). It contains HTML/CSS sample and sample MVC view - *Rates.cshtml*.

To configure shipping methods you have to go to *Commerce Manager* - *Administration* and under *Order System* - *Shipping* - *Shipping Methods* - *language* start creating new shipping method by clicking *New*. Provide shipping method details in *Overview* tab and use *Bring Shipping Gateway* as *Provider*. Click *OK* and open again newly created method. In *Parameters* tab provide Bring Product, customer number, ship from postal code, price rounding and other parameters.

![Configuration](https://github.com/Geta/Geta.Bring/blob/master/docs/config.png?raw=true)

## More info about Bring API

### Shipping Guide API

https://developer.bring.com/api/shipping-guide_2/

Related topics

https://developer.bring.com/api/shipping-guide_2/topics/

### Tracking API

http://developer.bring.com/api/tracking/

### Booking API

http://developer.bring.com/api/booking
