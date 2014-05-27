namespace TestForGolden
{
    public class OperationParameter
    {
        private OperationParameterValueMode mode;

        public string Name { get; set; }
        public OperationParameterValue Value { get; set; }

        public OperationParameterValueMode Mode
        {
            get { return mode; }
            set
            {
                mode = value;

                if (mode == OperationParameterValueMode.Constant
                    && !(Value is ConstantOperationParameterValue))
                {
                    Value = new ConstantOperationParameterValue();
                }
                    
            }
        }

        public OperationParameter()
        {
            Mode = OperationParameterValueMode.Constant;
        }

        public string GetValue()
        {
            return Value.GetValue();
        }
    }
}