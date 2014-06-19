using Olf.GoldenHorse.Foundation.Models;

namespace Olf.GoldenHorse.Core.Models
{
    public class LogGroupOperation : Operation
    {
        private OperationParameter nameParameter { get { return Parameters[0]; } }

        public string GroupName
        {
            get { return nameParameter.Value.ToString(); } 
            set { nameParameter.Value = value; }
        }

        public override string Name
        {
            get { return "Log Group"; }
        }

        public override string ParametersDescription
        {
            get { return nameParameter.Value.ToString(); }
        }

        protected override OperationParameter[] SetParameters()
        {
            OperationParameter param = new OperationParameter
            {
                Name = "Group Name",
                Mode = OperationParameterValueMode.Constant
            };

            return new[] { param };
        }

        public override string DefaultDescription(MappedItem control)
        {
            return "Creates a Log group named: " + nameParameter.Value;
        }

        public override bool Play(MappedItem control, Log log)
        {
            log.CreateLogItem(LogItemCategory.Event, nameParameter.GetValue());
            log.StartLogItemChildren();

            foreach (TestItem testItem in TestItem.Children)
            {
                if (!testItem.Play(log))
                    return false;
            }

            log.EndLogItemChildren();
            return true;
        }
    }
}