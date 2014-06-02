using System.Net.Mime;
using Olf.GoldenHorse.Foundation.Models;

namespace Olf.GoldenHorse.Core.Models
{
    public class KeyboardOperation : Operation
    {
        private OperationParameter textParam{ get { return Parameters[0]; } }

        public override string Name
        {
            get { return "Keyboard"; }
        }

        public override string ParametersDescription
        {
            get { return textParam.Value.ToString(); }
        }

        protected override OperationParameter[] SetParameters()
        {
            var param1 = new OperationParameter
            {
                Name="Text",
                Mode = OperationParameterValueMode.Constant
            };

            return new[] { param1 };
        }

        public override string DefaultDescription(MappedItem mappedItem)
        {
            return string.Format("Enters \"{0}\" int the '{1}' object", textParam.Value, mappedItem.FriendlyName);
        }

        public override void Play(MappedItem mappedItem)
        {
            throw new System.NotImplementedException();
        }


        public void SetText(string text)
        {
            textParam.Value = text;
        }

        
    }
}