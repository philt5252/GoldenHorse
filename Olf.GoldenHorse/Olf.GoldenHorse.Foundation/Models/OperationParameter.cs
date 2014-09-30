using System;
using System.Xml.Serialization;

namespace Olf.GoldenHorse.Foundation.Models
{
    public class OperationParameter
    {
        private OperationParameterValueMode mode;
        private OperationParameterValue parameterValue;
        private string typeIdentifier = "String";

        public string TypeIdentifier
        {
            get { return typeIdentifier; }
            set { typeIdentifier = value; }
        }


        public OperationParameterValue ParameterValue
        {
            get { return parameterValue; }
            set
            {
                parameterValue = value;
                parameterValue.OwningOperationParameter = this;
                OnValueChanged();
            }
        }

        public event EventHandler ValueChanged;

        public string Name { get; set; }

        [XmlIgnore]
        public object Value
        {
            get { return ParameterValue.DisplayValue; }
            set
            {
                this.ParameterValue.DisplayValue = value ?? "";
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
                    && !(this.ParameterValue is ConstantOperationParameterValue))
                {
                    this.ParameterValue = new ConstantOperationParameterValue();
                }
                else if (mode == OperationParameterValueMode.Variable
                    && !(this.ParameterValue is VariableOperationParameterValue))
                {
                    this.ParameterValue = new VariableOperationParameterValue();
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
            return ParameterValue.GetValue();
        }

        protected virtual void OnValueChanged()
        {
            EventHandler handler = ValueChanged;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }
    }
}