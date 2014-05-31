using System.Collections.Generic;
using System.Drawing;
using Olf.GoldenHorse.Foundation.Models;

namespace Olf.GoldenHorse.Core.Models
{
    public abstract class ClickOperation : Operation
    {
        private OperationParameter clickXParam;
        private OperationParameter clickYParam;

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

        protected override OperationParameter[] SetParameters()
        {
            clickXParam = new OperationParameter
            {
                Name = "ClientX",
                Mode = OperationParameterValueMode.Constant
            };

            clickYParam = new OperationParameter
            {
                Name = "ClientY",
                Mode = OperationParameterValueMode.Constant
            };

            return new [] {clickXParam, clickYParam};
        }
    }
}