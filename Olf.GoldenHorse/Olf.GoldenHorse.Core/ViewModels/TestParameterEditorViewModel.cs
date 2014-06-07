
using System.Linq;
using Olf.GoldenHorse.Foundation.Factories.ViewModels;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.ViewModels;

namespace Olf.GoldenHorse.Core.ViewModels
{
    public class TestParameterEditorViewModel : TabItemViewModel, ITestParameterEditorViewModel
    {
        private readonly TestItem testItem;
        private readonly OperationParameter[] parameters;

        public IOperationParameterViewModel[] Parameters { get; protected set; }

        public TestParameterEditorViewModel(TestItem testItem, 
            IOperationParameterViewModelFactory operationParameterViewModelFactory)
        {
            this.testItem = testItem;
            Parameters = testItem.Operation.Parameters
                .Select(operationParameterViewModelFactory.Create)
                .ToArray();
        }

        
    }
}