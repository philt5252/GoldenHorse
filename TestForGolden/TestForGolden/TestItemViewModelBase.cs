using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace TestForGolden
{
    public abstract class TestItemViewModelBase : ITestItemViewModel
    {
        public TestItem TestItem { get; set; }
        public virtual string Name { get; set; }
        public virtual string Operation { get; set; }
        public virtual string Value { get; set; }

        public virtual string Description
        {
            get { return TestItem.Description; }
            set { TestItem.Description = value; }
        }

        public virtual string AutowaitTimeout { get; set; }
        public IList<ITestItemViewModel> ChildItems { get; protected set; }

        protected TestItemViewModelBase()
        {
            ObservableCollection<ITestItemViewModel> testItemViewModels = new ObservableCollection<ITestItemViewModel>();
            ChildItems = testItemViewModels;
        }

        /*private void TestItemViewModelsOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            if (args.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (ITestItemViewModel newItem in args.NewItems)
                {
                    newItem.Parent = this;
                }
            }
            else if (args.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (ITestItemViewModel oldItem in args.OldItems)
                {
                    if (oldItem.Parent == this)
                        oldItem.Parent = null;
                }
            }
        }*/
    }
}