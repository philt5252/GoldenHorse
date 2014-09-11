using System;
using System.Linq;
using System.Threading;
using System.Xml.Serialization;
using ExpressionEvaluator;
using NCalc;
using Olf.GoldenHorse.Foundation.Models;

namespace Olf.GoldenHorse.Core.Models
{
    public class IfOperation : Operation
    {
        private OperationParameter expressionParam { get { return Parameters[0]; } }

        public override string Name
        {
            get { return "If"; }
        }

        [XmlIgnore]
        public bool EvaluatedResult { get; protected set; }

        public override string ParametersDescription
        {
            get { return expressionParam.Value.ToString(); }
        }

        protected override OperationParameter[] SetParameters()
        {
            var param1 = new OperationParameter
            {
                Name = "Boolean expression",
                Mode = OperationParameterValueMode.Constant
            };

            return new[] { param1 };
        }

        public override string DefaultDescription(MappedItem control)
        {
            return "Evaluates the boolean expression.";
        }

        public override bool Play(MappedItem control, Log log)
        {

            string expectedText = expressionParam.GetValue();

            if (expectedText == null)
                expectedText = "";

            foreach (Variable v in this.TestItem.Test.Variables.OrderBy(v => v.Name.Length))
            {
                expectedText = expectedText.Replace(v.Name, "\"" + v.Value + "\"");
            }

            var expression = new CompiledExpression(expectedText);

            try
            {
                EvaluatedResult = (bool)expression.Eval();

            }
            catch (Exception ex)
            {
                //expression was not a boolean
            }

            if (EvaluatedResult)
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