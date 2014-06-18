using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;

namespace Olf.GoldenHorse.Foundation.Models
{
    public class VariableOperationParameterValue : OperationParameterValue
    {
        public override string GetValue()
        {
            IEnumerable<Variable> variables = this.OwningOperationParameter.OwningTestItem.Test.Variables;

            Match match = Regex.Match(DisplayValue, @"(.+)\[(.+)\]");

            if (!match.Success)
            {
                Variable variable = variables.FirstOrDefault(v => Equals(v.Name, DisplayValue));

                if (variable == null)
                    throw new Exception(string.Format("Variable named {0} was not found.", DisplayValue));

                return variable.Value.ToString();
            }
            else
            {
                string varName =  match.Groups[1].Captures[0].Value;
                string colName =  match.Groups[2].Captures[0].Value;

                Variable variable = variables.FirstOrDefault(v => Equals(v.Name, varName));

                if(variable == null)
                    throw new Exception(string.Format("Variable named {0} was not found.", DisplayValue));

                return variable.DataTableValue.Rows[0][colName].ToString();
            }

            
        }
    }
}