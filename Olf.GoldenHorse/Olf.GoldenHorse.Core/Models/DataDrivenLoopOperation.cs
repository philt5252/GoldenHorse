using Olf.GoldenHorse.Foundation.Models;

namespace Olf.GoldenHorse.Core.Models
{
    public class DataDrivenLoopOperation : Operation
    {
        private OperationParameter variableParameter { get { return Parameters[0]; } }

        public override string Name
        {
            get { return "Data Driven Loop"; }
        }

        public override string ParametersDescription
        {
            get { return ""; }
        }

        protected override OperationParameter[] SetParameters()
        {
            OperationParameter param = new OperationParameter
            {
                Name = "Table Variable",
                Mode = OperationParameterValueMode.Variable
            };

            return new []{param};
        }

        public override string DefaultDescription(MappedItem control)
        {
            return "";
        }

        public override bool Play(MappedItem control, Log log)
        {
            VariableOperationParameterValue variableOperationParameterValue = variableParameter.ParameterValue as VariableOperationParameterValue;

            Variable variable = variableOperationParameterValue.GetVariable();

            variable.CurrentTableRow = 0;

            for (int i = 0; i < variable.DataTableValue.Rows.Count; i++)
            {
                foreach (TestItem testItem in TestItem.Children)
                {
                    if (!testItem.Play(log))
                        return false;
                }

                variable.CurrentTableRow++;
            }
            
            variable.CurrentTableRow = 0;

            return true;
        }
    }
}