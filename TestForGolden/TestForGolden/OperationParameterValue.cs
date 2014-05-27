namespace TestForGolden
{
    public abstract class OperationParameterValue
    {
        public string DisplayValue { get; set; }
        public abstract string GetValue();
    }
}