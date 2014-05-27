using System.Windows.Media;

namespace TestForGolden
{
    public class ClickOperation : Operation
    {
        public ClickOperation()
        {
            OperationParameter param1 = new OperationParameter { Name = "ClientX" };
            param1.Mode = OperationParameterValueMode.Constant;
            OperationParameter param2 = new OperationParameter { Name = "ClientY" };
            param2.Mode = OperationParameterValueMode.Constant;

            Parameters.Add(param1);
            Parameters.Add(param2);
        }
    }
}