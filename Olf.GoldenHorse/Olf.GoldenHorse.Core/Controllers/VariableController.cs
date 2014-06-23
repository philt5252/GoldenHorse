using Olf.GoldenHorse.Foundation.Controllers;
using Olf.GoldenHorse.Foundation.Factories.ViewModels;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.ViewModels;
using Olf.GoldenHorse.Foundation.Views;
using Olf.GoldenHorse.Foundation.Views.Factories;

namespace Olf.GoldenHorse.Core.Controllers
{
    public class VariableController : IVariableController
    {
        private readonly IVariableTableWindowFactory variableTableWindowFactory;
        private readonly IVariableTableEditViewModelFactory variableTableEditViewModelFactory;
        private IWindow variableTableEditWindow;

        public VariableController(IVariableTableWindowFactory variableTableWindowFactory,
            IVariableTableEditViewModelFactory variableTableEditViewModelFactory)
        {
            this.variableTableWindowFactory = variableTableWindowFactory;
            this.variableTableEditViewModelFactory = variableTableEditViewModelFactory;
        }

        public void EditTableVariable(Variable variable)
        {
            variableTableEditWindow = variableTableWindowFactory.Create();
            IVariableTableEditViewModel variableTableEditViewModel = variableTableEditViewModelFactory.Create(variable);

            variableTableEditWindow.DataContext = variableTableEditViewModel;

            variableTableEditWindow.ShowDialog();
        }

        public void CloseTableEditView()
        {
            variableTableEditWindow.Close();
        }
    }
}