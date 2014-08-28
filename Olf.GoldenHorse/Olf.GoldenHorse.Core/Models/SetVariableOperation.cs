using System.Linq;
using Olf.GoldenHorse.Foundation.Models;

namespace Olf.GoldenHorse.Core.Models
{
    public class SetVariableOperation : Operation
    {
        private OperationParameter variableParam { get { return Parameters[0]; }}
        private OperationParameter valueParam { get { return Parameters[1]; } }


        public override string Name
        {
            get { return "Set Variable"; }
        }

        public override string ParametersDescription
        {
            get { return string.Format("{0}, {1}", variableParam.Value, valueParam.Value); }
        }

        protected override OperationParameter[] SetParameters()
        {
            var param1 = new OperationParameter
            {
                Name = "Variable",
                Mode = OperationParameterValueMode.Variable,
            };

            var param2 = new OperationParameter
            {
                Name = "Value",
                Mode = OperationParameterValueMode.Constant,
            };

            return new[] {param1, param2};
        }

        public override string DefaultDescription(MappedItem control)
        {
            return string.Format("Set variable {0} to value {1}", variableParam.Value, valueParam.Value);
        }

        public override bool Play(MappedItem control, Log log)
        {
            Variable variable = this.TestItem.Test.Variables.FirstOrDefault(v => v.Name.Equals(variableParam.Value));
            variable.Value = valueParam.GetValue();

            log.CreateLogItem(LogItemCategory.Message, string.Format("Variable '{0}' was set to value: {1}", variable.Name, variable.Value));

            return true;

        }
    }
}