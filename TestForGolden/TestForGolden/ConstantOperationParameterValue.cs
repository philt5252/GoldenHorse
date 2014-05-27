namespace TestForGolden
{
    public class ConstantOperationParameterValue : OperationParameterValue
    {
        public override string GetValue()
        {
            return DisplayValue;
        }
    }
}