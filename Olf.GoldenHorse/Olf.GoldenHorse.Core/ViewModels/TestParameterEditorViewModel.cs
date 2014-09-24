
using System;
using System.Linq;
using Olf.GoldenHorse.Foundation.Factories.ViewModels;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.ViewModels;

namespace Olf.GoldenHorse.Core.ViewModels
{
    public class TestParameterEditorViewModel : ViewModelBase, ITestParameterEditorViewModel
    {
        private readonly TestItem testItem;
        private readonly IOperationParameterViewModelFactory operationParameterViewModelFactory;
        private IOperationParameterViewModel[] parameters;

        public IOperationParameterViewModel[] Parameters
        {
            get { return parameters; }
            protected set
            {
                parameters = value;
                OnPropertyChanged("Parameters");
            }
        }

        public TestParameterEditorViewModel(TestItem testItem, 
            IOperationParameterViewModelFactory operationParameterViewModelFactory)
        {
            this.testItem = testItem;
            this.operationParameterViewModelFactory = operationParameterViewModelFactory;
            if (testItem.Operation != null)
            {
                Parameters = testItem.Operation.Parameters
                    .Select(operationParameterViewModelFactory.Create)
                    .ToArray();
            }

            testItem.OperationChanged += TestItemOnOperationChanged;
        }

        private void TestItemOnOperationChanged(object sender, EventArgs eventArgs)
        {
            if (testItem.Operation != null)
            {
                Parameters = testItem.Operation.Parameters
                    .Select(operationParameterViewModelFactory.Create)
                    .ToArray();
            }
        }
    }
}