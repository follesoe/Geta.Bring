namespace Geta.Bring.Shipping.Model.Errors
{
    public class Error
    {
        public string Code { get; set; }
        public override string ToString()
        {
            return Code;
        }
    }
}
