
using System;
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
                testItem.Description = value;
                OnPropertyChanged("Description");
            }
        }

        public TestDescriptionEditorViewModel(TestItem testItem)
        {
            this.testItem = testItem;
            testItem.DescriptionChanged += TestItemOnDescriptionChanged;
        }

        private void TestItemOnDescriptionChanged(object sender, EventArgs eventArgs)
        {
            OnPropertyChanged("Description");
        }
    }
}