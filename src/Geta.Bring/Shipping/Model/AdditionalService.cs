using System.Collections.Generic;
using System.Linq;

namespace Geta.Bring.Shipping.Model
{
    /// <summary>
    /// Additional services from http://developer.bring.com/additionalresources/productlist.html?from=shipping .
    /// </summary>
    public class AdditionalService
    {
        private AdditionalService(string displayName, string code, IEnumerable<Product> appliesTo)
        {
            AppliesTo = appliesTo;
            Code = code;
            DisplayName = displayName;
        }

        /// <summary>
        /// Additional service's display name.
        /// </summary>
        public string DisplayName { get; }

        /// <summary>
        /// Additional service's code.
        /// </summary>
        public string Code { get; }

        /// <summary>
        /// List of products to which additional service applies.
        /// </summary>
        public IEnumerable<Product> AppliesTo { get; }

        public static AdditionalService Evarsling = 
            new AdditionalService("Recipient notification over SMS or E-Mail", "EVARSLING", new[]
            {
                Product.BpakkeDorDor,
                Product.Servicepakke,
                Product.Express09
            });
        public static AdditionalService Postoppkrav =
            new AdditionalService("Cash on Delivery", "POSTOPPKRAV", new[]
            {
                Product.APost,
                Product.BPost,
                Product.BpakkeDorDor,
                Product.Servicepakke,
                Product.PaDoren,
                Product.Express09
            });
        public static AdditionalService Lordagsutkjoring =
            new AdditionalService("Delivery on Saturdays", "LORDAGSUTKJORING", new[]
            {
                Product.Express09
            });
        public static AdditionalService Envelope = 
            new AdditionalService("QuickPack Envelope", "ENVELOPE", new[]
            {
                Product.QuickpackOverNight0900,
                Product.QuickpackOverNight1200,
                Product.QuickpackDayCertain
            });
        public static AdditionalService Advisering = 
            new AdditionalService("Bring contacts recipient", "ADVISERING", new[]
            {
                Product.CargoGroupage
            });
        public static AdditionalService PickupPoint = 
            new AdditionalService("Delivery to pickup point", "PICKUP_POINT", new[]
            {
                Product.CarryonHomeshopping
            });

        /// <summary>
        /// Returns all available additional services.
        /// </summary>
        public static IEnumerable<AdditionalService> All
        {
            get
            {
                yield return Evarsling;
                yield return Postoppkrav;
                yield return Lordagsutkjoring;
                yield return Envelope;
                yield return Advisering;
                yield return PickupPoint;
            }
        }

        /// <summary>
        /// Gets additional service by it's code.
        /// </summary>
        /// <param name="code">Additional service code.</param>
        /// <returns>Found <see cref="AdditionalService"/></returns>
        public static AdditionalService GetByCode(string code)
        {
            return All.First(x => x.Code == code);
        }
    }
}