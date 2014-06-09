using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Olf.GoldenHorse.Foundation.Models
{
    public class VariableOperationParameterValue : OperationParameterValue
    {
        public override string GetValue()
        {
            IEnumerable<Variable> variables = this.OwningOperationParameter.OwningTestItem.Test.Variables;
            Variable variable = variables.FirstOrDefault(v => Equals(v.Name, DisplayValue));

            if(variable == null)
                throw new Exception(string.Format("Variable named {0} was not found.", DisplayValue));

            return variable.Value.ToString();
        }
    }
}