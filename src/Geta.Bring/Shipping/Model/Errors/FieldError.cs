namespace Geta.Bring.Shipping.Model.Errors
{
    public class FieldError : Error
    {
        public string Message { get; set; }
        public string Field { get; set; }
    }
}
