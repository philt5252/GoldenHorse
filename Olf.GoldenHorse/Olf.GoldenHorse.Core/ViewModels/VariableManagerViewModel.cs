
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Olf.GoldenHorse.Foundation.Controllers;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.ViewModels;

namespace Olf.GoldenHorse.Core.ViewModels
{
    public class VariableManagerViewModel : IVariableManagerViewModel
    {
        private readonly Test test;
        private readonly IVariableController variableController;

        public ObservableCollection<Variable> Variables { get; protected set; } 
        public Variable SelectedVariable { get; set; }
        public static VariableType[] Types { get; protected set; }

        public ICommand EditTableVariableCommand { get; protected set; }

        static VariableManagerViewModel()
        {
            Types = Enum.GetValues(typeof (VariableType)).Cast<VariableType>().ToArray();
        }

        public VariableManagerViewModel(Test test, IVariableController variableController)
        {
            this.test = test;
            this.variableController = variableController;
            Variables = test.Variables;
            EditTableVariableCommand = new DelegateCommand(ExecuteEditTableVariableCommand);
        }

        private void ExecuteEditTableVariableCommand()
        {
            variableController.EditTableVariable(SelectedVariable);
        }
    }
}