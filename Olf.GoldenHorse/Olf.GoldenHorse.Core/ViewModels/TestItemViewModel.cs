using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Olf.GoldenHorse.Foundation.Controllers;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.ViewModels;

namespace Olf.GoldenHorse.Core.ViewModels
{
    public class TestItemViewModel : ITestItemViewModel
    {
        private readonly ITestItemController testItemController;
        private string controlId;
        private string name;
        private string description;
        private string type;

        public TestItem TestItem { get; set; }

        public string Type
        {
            get { return TestItem == null ? type : TestItem.Type; }
            set
            {
                type = value;

                if (TestItem != null)
                    TestItem.Type = type;
            }
        }

        public string ControlId
        {
            get { return TestItem == null ? controlId : TestItem.ControlId; }
            set
            {
                controlId = value;

                if(TestItem != null)
                    TestItem.ControlId = value;
            }
        }

        public IList<ITestItemViewModel> ChildItems { get; protected set; }
        public Screenshot Screenshot { get { return TestItem == null ? null : TestItem.Screenshot; } }
        public bool HasScreenshot { get { return Screenshot != null; } }

        public virtual string Name
        {
            get { return TestItem == null ? name : TestItem.Control.FriendlyName; }
            set
            {
                name = value;

                if (TestItem != null)
                    TestItem.Control.FriendlyName = value;
            }
        }

        public virtual string Operation { get { return TestItem == null ? "" : TestItem.Operation.Name; } }
        public virtual string Value { get { return TestItem == null ? "" : TestItem.Operation.ParametersDescription; } }

        public virtual string Description
        {
            get { return TestItem == null ? description : TestItem.Description; }
            set
            {
                description = value;

                if(TestItem != null)
                    TestItem.Description = value;
            }
        }

        public virtual string AutowaitTimeout { get; set; }

        public ICommand EditObjectCommand { get; protected set; }
        public ICommand EditOperationCommand { get; protected set; }
        public ICommand EditParameterCommand { get; protected set; }
        public ICommand EditDescriptionCommand { get; protected set; }

        public TestItemViewModel(TestItem testItem, ITestItemController testItemController)
        {
            this.testItemController = testItemController;
            TestItem = testItem;
            ObservableCollection<ITestItemViewModel> testItemViewModels = new ObservableCollection<ITestItemViewModel>();
            ChildItems = testItemViewModels;

            EditObjectCommand = new DelegateCommand(ExecuteEditObjectCommand);
            EditOperationCommand = new DelegateCommand(ExecuteEditOperationCommand);
            EditParameterCommand = new DelegateCommand(ExeccuteEditParameterCommand);
            EditDescriptionCommand = new DelegateCommand(ExecuteEditDescriptionCommand);
        }

        private void ExecuteEditDescriptionCommand()
        {
            testItemController.EditTestItem(TestItem);
        }

        private void ExeccuteEditParameterCommand()
        {
            testItemController.EditTestItem(TestItem);
        }

        private void ExecuteEditOperationCommand()
        {
            testItemController.EditTestItem(TestItem);
        }

        private void ExecuteEditObjectCommand()
        {
            testItemController.EditTestItem(TestItem);
        }
    }
}