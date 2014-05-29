namespace Olf.GoldenHorse.Foundation.Models
{
    public abstract class OperationParameterValue
    {
        public string DisplayValue { get; set; }
        public abstract string GetValue();
    }
}