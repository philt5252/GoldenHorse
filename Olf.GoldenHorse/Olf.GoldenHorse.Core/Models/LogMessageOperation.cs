using System;
using System.Threading;
using Olf.GoldenHorse.Foundation.Models;

namespace Olf.GoldenHorse.Core.Models
{
    public class LogMessageOperation : Operation
    {
        private OperationParameter messageParameter { get { return Parameters[0]; } }

        public override string Name
        {
            get { return "Message"; }
        }

        public string Message 
        {
            get{ return messageParameter.Value.ToString(); }

            set { messageParameter.Value = value; }
        }

        public override string ParametersDescription
        {
            get { return messageParameter.Value.ToString(); }
        }

        protected override OperationParameter[] SetParameters()
        {
            var param1 = new OperationParameter
            {
                Name = "Message",
                Mode = OperationParameterValueMode.Constant
            };

            return new[] { param1 };
        }

        public override string DefaultDescription(MappedItem control)
        {
            return string.Format("Message: {0}", messageParameter.Value);
        }

        public override bool Play(MappedItem control, Log log)
        {

            log.CreateLogItem(LogItemCategory.Event, string.Format("Message: {0}", messageParameter.GetValue()));

            return true;
        }
    }
}