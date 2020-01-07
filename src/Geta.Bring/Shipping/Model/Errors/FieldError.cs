namespace Geta.Bring.Shipping.Model.Errors
{
    public class FieldError : Error
    {
        public FieldError(string code, string message, string field) : base(code)
        {
            Message = message;
            Field = field;
        }

        public string Message { get; }
        public string Field { get;}

        public override string ToString()
        {
            return Code + ": '" + Field + "' " + Message;
        }
    }
}
