using Olf.GoldenHorse.Foundation.Models;

namespace Olf.GoldenHorse.Core.Models
{
    public class KeyboardOperation : Operation
    {
        private OperationParameter textParam;

        public override string Name
        {
            get { return "Keyboard"; }
        }

        public KeyboardOperation()
        {
            textParam = new OperationParameter
            {
                Name="Text",
                Mode = OperationParameterValueMode.Constant
            };

            Parameters.Add(textParam);
        }

        public void SetText(string text)
        {
            textParam.Value = text;
        }

        
    }
}