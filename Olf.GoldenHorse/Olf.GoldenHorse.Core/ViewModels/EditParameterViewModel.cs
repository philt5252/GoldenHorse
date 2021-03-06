
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Documents;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Olf.GoldenHorse.Foundation.Controllers;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.ViewModels;

namespace Olf.GoldenHorse.Core.ViewModels
{
    public class EditParameterViewModel : IEditParameterViewModel
    {
        private readonly OperationParameter parameter;
        private readonly ITestItemController testItemController;
        public string[] ValidationModes { get; protected set; }
        private string selectedValidationMode;
        private string parameterValue;

        public string[] Variables { get; protected set; }
        public string SelectedVariable { get; set; }

        public string SelectedValidationMode
        {
            get
            {
                return selectedValidationMode;
            }
            set
            {
                selectedValidationMode = value;
            }
        }

        public string Value
        {
            get
            {
                return parameterValue;
            }
            set
            {
                parameterValue = value;
            }
        }

        public string Name { get { return parameter.Name; } }
        public ICommand SaveParameterCommand { get; protected set; }
        public ICommand CancelParameterCommand { get; protected set; }

        public EditParameterViewModel(OperationParameter parameter,
            ITestItemController  testItemController)
        {
            this.parameter = parameter;
            this.testItemController = testItemController;

            ValidationModes = Enum.GetNames(typeof (OperationParameterValueMode))
                .ToArray();

            SaveParameterCommand = new DelegateCommand(ExecuteSaveParameterCommand);
            CancelParameterCommand = new DelegateCommand(ExecuteCancelParameterCommand);

            Value = parameter.Value.ToString();
            SelectedValidationMode = parameter.Mode.ToString();

            List<string> vars = new List<string>();

            foreach (Variable variable in parameter.OwningTestItem.Test.Variables)
            {
                vars.Add(variable.Name);
                if (variable.VariableType == VariableType.TableValue)
                {
                    foreach (DataColumn col in variable.DataTableValue.Columns)
                    {
                        vars.Add(variable.Name + "[" + col.ColumnName + "]");
                    }
                }
            }

            Variables = vars.ToArray();
        }

        private void ExecuteCancelParameterCommand()
        {
            testItemController.CloseEditParameterWindow();
        }

        private void ExecuteSaveParameterCommand()
        {
            parameter.Mode = (OperationParameterValueMode)Enum.Parse(typeof (OperationParameterValueMode), SelectedValidationMode);
            
            if(Equals(SelectedValidationMode, OperationParameterValueMode.Constant.ToString()))
                parameter.Value = parameterValue;
            else if (Equals(SelectedValidationMode, OperationParameterValueMode.Variable.ToString()))
                parameter.Value = SelectedVariable;

            testItemController.CloseEditParameterWindow();
        }
    }
}