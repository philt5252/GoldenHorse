using System.Collections.Generic;
using System.Collections.ObjectModel;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.ViewModels;

namespace Olf.GoldenHorse.Core.ViewModels
{
    public class TestItemViewModel : ITestItemViewModel
    {
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

        public TestItemViewModel(TestItem testItem)
        {
            TestItem = testItem;
            ObservableCollection<ITestItemViewModel> testItemViewModels = new ObservableCollection<ITestItemViewModel>();
            ChildItems = testItemViewModels;
        }
    }
}