using System;

namespace Geta.Bring.Booking.Model
{
    /// <summary>
    /// Nature of transaction need to be set on <see cref="Geta.Bring.Booking.Model.CustomsDeclarations.NatureOfTransaction" />.
    /// The value is converted to the correct string value using <see cref="Geta.Bring.Booking.Infrastructure.NatureOfTransactionConverter" />.
    /// </summary>
    public enum NatureOfTransaction
    {
        SaleOfGoods,
        ReturnedGoods,
        Gift,
        CommercialSample,
        Documents,
        Other
    }
}