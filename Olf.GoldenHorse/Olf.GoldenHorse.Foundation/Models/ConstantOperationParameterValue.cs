namespace Olf.GoldenHorse.Foundation.Models
{
    public class ConstantOperationParameterValue : OperationParameterValue
    {
        public override string GetValue()
        {
            return DisplayValue.ToString();
        }
    }
}