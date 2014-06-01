using System.Collections.Generic;
using System.Drawing;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.Services;
using TestStack.White.UIItems;

namespace Olf.GoldenHorse.Core.Models
{
    public abstract class ClickOperation : Operation
    {
        private OperationParameter clickXParam { get { return Parameters[0]; } }
        private OperationParameter clickYParam { get { return Parameters[1]; } }

        public override string ParametersDescription
        {
            get { return string.Format("{0}, {1}", clickXParam.Value, clickYParam.Value); }
        }

        protected ClickOperation()
        {
            
        }

        public void SetClickPoint(int x, int y)
        {
            clickXParam.Value = x;
            clickYParam.Value = y;
        }

        public Point GetClickPoint()
        {
            return new Point(int.Parse(clickXParam.GetValue()), int.Parse(clickYParam.GetValue()));
        }

        public override string DefaultDescription(string windowId, string controlId)
        {
            return string.Format("Click on {0} at ({1})", controlId, ParametersDescription);
        }

        protected override OperationParameter[] SetParameters()
        {
            var param1 = new OperationParameter
            {
                Name = "ClientX",
                Mode = OperationParameterValueMode.Constant
            };

            var param2 = new OperationParameter
            {
                Name = "ClientY",
                Mode = OperationParameterValueMode.Constant
            };

            return new[] { param1, param2 };
        }
    }
}