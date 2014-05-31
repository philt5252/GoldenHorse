using System.Net.Mime;
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

        protected override OperationParameter[] SetParameters()
        {
            textParam = new OperationParameter
            {
                Name="Text",
                Mode = OperationParameterValueMode.Constant
            };

            return new []{textParam};
        }


        public void SetText(string text)
        {
            textParam.Value = text;
        }

        
    }
}