
using System;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Olf.GoldenHorse.Foundation.Controllers;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.ViewModels;

namespace Olf.GoldenHorse.Core.ViewModels
{
    public class OperationParameterViewModel : IOperationParameterViewModel
    {
        private readonly OperationParameter parameter;
        private readonly ITestItemController testItemController;

        public string Name { get { return parameter.Name; } }
        public string Value { get { return parameter.Value.ToString(); } }

        public ICommand EditParameterCommand { get; protected set; }

        public OperationParameterViewModel(OperationParameter parameter,
            ITestItemController testItemController)
        {
            this.parameter = parameter;
            this.testItemController = testItemController;

            EditParameterCommand = new DelegateCommand(ExecuteEditParameterCommand);
        }

        private void ExecuteEditParameterCommand()
        {
            testItemController.EditParameter(parameter);
        }
    }
}