using Olf.GoldenHorse.Foundation.Controllers;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.Views;
using Olf.GoldenHorse.Foundation.Views.Factories;

namespace Olf.GoldenHorse.Core.Controllers
{
    public class VariableController : IVariableController
    {
        private readonly IVariableTableWindowFactory variableTableWindowFactory;

        public VariableController(IVariableTableWindowFactory variableTableWindowFactory)
        {
            this.variableTableWindowFactory = variableTableWindowFactory;
        }

        public void EditTableVariable(Variable variable)
        {
            IWindow window = variableTableWindowFactory.Create();

            window.ShowDialog();
        }
    }
}