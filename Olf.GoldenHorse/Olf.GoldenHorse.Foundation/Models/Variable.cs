namespace Olf.GoldenHorse.Foundation.Models
{
    public class Variable
    {
        public string Name { get; set; }
        public VariableType VariableType { get; set; }
        public object DefaultValue { get; set; }
        public string Description { get; set; }
    }

    public enum VariableType
    {
        SingleValue,
        TableValue
    }
}