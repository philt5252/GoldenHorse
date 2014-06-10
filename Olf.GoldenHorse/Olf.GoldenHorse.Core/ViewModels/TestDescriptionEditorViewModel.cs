
using System;
using System.Windows.Input;
using Microsoft.Expression.Interactivity.Core;
using Microsoft.Practices.Prism.Commands;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.ViewModels;

namespace Olf.GoldenHorse.Core.ViewModels
{
    public class TestDescriptionEditorViewModel : ViewModelBase, ITestDescriptionEditorViewModel
    {
        private readonly TestItem testItem;
        private string description;

        public string Description
        {
            get
            {
                return testItem.Description;
            }
            set
            {
                testItem.DescriptionOverride = value;
                OnPropertyChanged("Description");
            }
        }

        public ICommand DefaultDescriptionCommand { get; protected set; }

        public TestDescriptionEditorViewModel(TestItem testItem)
        {
            this.testItem = testItem;
            testItem.DescriptionChanged += TestItemOnDescriptionChanged;

            DefaultDescriptionCommand = new DelegateCommand(ExecuteDefaultDescriptionCommand);
        }

        private void ExecuteDefaultDescriptionCommand()
        {
            Description = null;
        }

        private void TestItemOnDescriptionChanged(object sender, EventArgs eventArgs)
        {
            OnPropertyChanged("Description");
        }
    }
}