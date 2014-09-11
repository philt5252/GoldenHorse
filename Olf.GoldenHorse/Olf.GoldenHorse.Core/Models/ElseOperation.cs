using System;
using System.Linq;
using ExpressionEvaluator;
using Olf.GoldenHorse.Foundation.Models;

namespace Olf.GoldenHorse.Core.Models
{
    public class ElseOperation : Operation
    {
        public override string Name
        {
            get { return "Else"; }
        }

        public override string ParametersDescription
        {
            get { return ""; }
        }

        protected override OperationParameter[] SetParameters()
        {
            return new OperationParameter[0];
        }

        public override string DefaultDescription(MappedItem control)
        {
            return "Evaluates the boolean expression.";
        }

        public override bool Play(MappedItem control, Log log)
        {
            var previousTestItems = TestItem.Parent.TestItems.TakeWhile(t => !t.Equals(this.TestItem)).Reverse();

            TestItem ifTestItem = previousTestItems.FirstOrDefault(t => t.Operation is IfOperation);

            bool execute = ifTestItem == null || !(ifTestItem.Operation as IfOperation).EvaluatedResult;

            if (execute)
            {
                foreach (TestItem testItem in this.TestItem.TestItems)
                {
                    testItem.Play(log);
                }
            }

            return true;
        }
    }
}