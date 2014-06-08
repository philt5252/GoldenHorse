
using System;
using System.Linq;
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
        }

        private void ExecuteCancelParameterCommand()
        {
            testItemController.CloseEditParameterWindow();
        }

        private void ExecuteSaveParameterCommand()
        {
            parameter.Mode = (OperationParameterValueMode)Enum.Parse(typeof (OperationParameterValueMode), SelectedValidationMode);
            parameter.Value = parameterValue;
            testItemController.CloseEditParameterWindow();
        }
    }
}