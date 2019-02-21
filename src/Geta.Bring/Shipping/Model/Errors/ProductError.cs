using Newtonsoft.Json;

namespace Geta.Bring.Shipping.Model.Errors
{
    public class ProductError : Error
    {        
        [JsonConstructor]
        public ProductError(string code, string description) : base(code)
        {
            Description = description;
        }

        public string Description { get; set; }
    }
}
