using System.Data;
using System.Xml.Serialization;

namespace Olf.GoldenHorse.Foundation.Models
{
    public class Variable
    {
        private object value = null;
        public string Name { get; set; }
        public VariableType VariableType { get; set; }
        public object DefaultValue { get; set; }

        [XmlIgnore]
        public int CurrentTableRow { get; set; }

        public DataTable DataTableValue { get; set; }

        public object Value
        {
            get { return value ?? DefaultValue; }
            set { this.value = value; }
        }

        public void Reset()
        {
            Value = null;
        }

        public string Description { get; set; }
    }

    public enum VariableType
    {
        SingleValue,
        TableValue
    }
}