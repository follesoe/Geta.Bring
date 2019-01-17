using Newtonsoft.Json;

namespace Geta.Bring.Shipping.Model.Errors
{
    public class Error
    {
        public Error(string code)
        {
            Code = code;
        }

        public string Code { get; set; }
        public override string ToString()
        {
            return Code;
        }
    }
}
