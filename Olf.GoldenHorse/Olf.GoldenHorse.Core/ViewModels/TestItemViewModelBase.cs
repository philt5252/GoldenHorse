using System.Collections.Generic;
using System.Collections.ObjectModel;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.ViewModels;

namespace Olf.GoldenHorse.Core.ViewModels
{
    public abstract class TestItemViewModelBase : ITestItemViewModel
    {
        public TestItem TestItem { get; set; }
        public abstract string Name { get; }
        public abstract string Operation { get; }
        public virtual string Value { get; set; }

        public virtual string Description
        {
            get { return TestItem.Description; }
            set { TestItem.Description = value; }
        }

        public virtual string AutowaitTimeout { get; set; }
        public IList<ITestItemViewModel> ChildItems { get; protected set; }
        public Screenshot Screenshot { get { return TestItem == null ? null : TestItem.Screenshot; } }
        public bool HasScreenshot { get { return Screenshot != null; } }

        protected TestItemViewModelBase()
        {
            ObservableCollection<ITestItemViewModel> testItemViewModels = new ObservableCollection<ITestItemViewModel>();
            ChildItems = testItemViewModels;
        }
    }
}