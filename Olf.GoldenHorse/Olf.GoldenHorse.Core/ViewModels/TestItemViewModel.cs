using System;
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
        public TestItem TestItem { get; set; }

        public IList<ITestItemViewModel> ChildItems { get; protected set; }
        public Screenshot Screenshot { get { return TestItem == null ? null : TestItem.Screenshot; } }
        public bool HasScreenshot { get { return Screenshot != null; } }

        public virtual string Name
        {
            get { return TestItem.Control.FriendlyName; }
        }

        public virtual string Operation { get { return TestItem.Operation.Name; } }
        public virtual string Value { get { return TestItem.Operation.ParametersDescription; } }

        public virtual string Description
        {
            get { return TestItem.Description; }
            set { TestItem.Description = value; }
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