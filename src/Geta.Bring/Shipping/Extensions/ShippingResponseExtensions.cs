using System;
using System.Collections.Generic;
using System.Linq;
using Geta.Bring.Shipping.Model;
using Geta.Bring.Shipping.Model.Errors;

namespace Geta.Bring.Shipping.Extensions
{
    internal static class ShippingResponseExtensions
    {
        internal static IEnumerable<ProductResponse> GetAllProducts(this ShippingResponse response)
        {
            if (response == null) throw new ArgumentNullException(nameof(response));
            return response.Consignments.SelectMany(x => x.Products);
        }

        internal static IEnumerable<Error> GetAllErrors(this ShippingResponse response)
        {
            if (response == null) throw new ArgumentNullException(nameof(response));

            IEnumerable<Error> fieldErrors = response.FieldErrors;
            IEnumerable<Error> productErrors = GetAllProducts(response).SelectMany(x => x.Errors);

            return fieldErrors.Concat(productErrors);
        }
    }
}