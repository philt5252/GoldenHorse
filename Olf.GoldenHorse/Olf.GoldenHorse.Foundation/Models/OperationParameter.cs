using System;
using System.Xml.Serialization;

namespace Olf.GoldenHorse.Foundation.Models
{
    public class OperationParameter
    {
        private OperationParameterValueMode mode;
        private OperationParameterValue value;

        public event EventHandler ValueChanged;

        public string Name { get; set; }

        public object Value
        {
            get { return value.DisplayValue; }
            set
            {
                this.value.DisplayValue = value.ToString();
                OnValueChanged();
            }
        }

        public OperationParameterValueMode Mode
        {
            get { return mode; }
            set
            {
                mode = value;

                if (mode == OperationParameterValueMode.Constant
                    && !(this.value is ConstantOperationParameterValue))
                {
                    this.value = new ConstantOperationParameterValue();
                }

                OnValueChanged();
            }
        }

        [XmlIgnore]
        public Operation OwningOperation { get; set; }

        [XmlIgnore]
        public TestItem OwningTestItem { get { return OwningOperation.TestItem; } }

        public OperationParameter()
        {
            Mode = OperationParameterValueMode.Constant;
        }

        public string GetValue()
        {
            return value.GetValue();
        }

        protected virtual void OnValueChanged()
        {
            EventHandler handler = ValueChanged;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }
    }
}