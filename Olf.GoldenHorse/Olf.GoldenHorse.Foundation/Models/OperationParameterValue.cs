using System.Xml.Serialization;

namespace Olf.GoldenHorse.Foundation.Models
{
    public abstract class OperationParameterValue
    {
        private object displayValue;

        [XmlIgnore]
        public OperationParameter OwningOperationParameter { get; set; }

        public object DisplayValue
        {
            get { return displayValue; }
            set { displayValue = value; }
        }

        public abstract string GetValue();

        protected OperationParameterValue()
        {
            DisplayValue = "";
        }
    }
}