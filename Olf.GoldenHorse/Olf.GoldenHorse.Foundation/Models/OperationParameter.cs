namespace Olf.GoldenHorse.Foundation.Models
{
    public class OperationParameter
    {
        private OperationParameterValueMode mode;
        private OperationParameterValue value;
        

        public string Name { get; set; }

        public object Value
        {
            get { return value.DisplayValue; }
            set
            {
                this.value.DisplayValue = value.ToString();
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

            }
        }

        public OperationParameter()
        {
            Mode = OperationParameterValueMode.Constant;
        }

        public string GetValue()
        {
            return value.GetValue();
        }
    }
}