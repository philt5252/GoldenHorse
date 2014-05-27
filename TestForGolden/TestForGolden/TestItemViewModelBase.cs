using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace TestForGolden
{
    public abstract class TestItemViewModelBase : ITestItemViewModel
    {
        public abstract ITestItemViewModel Parent { get; set; }
        public abstract string Name { get; set; }
        public abstract string Operation { get; set; }
        public abstract string Value { get; set; }
        public abstract string Description { get; set; }
        public abstract string AutowaitTimeout { get; set; }
        public IList<ITestItemViewModel> ChildItems { get; protected set; }

        protected TestItemViewModelBase()
        {
            ObservableCollection<ITestItemViewModel> testItemViewModels = new ObservableCollection<ITestItemViewModel>();
            ChildItems = testItemViewModels;

            testItemViewModels.CollectionChanged += TestItemViewModelsOnCollectionChanged;
        }

        private void TestItemViewModelsOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
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
        }
    }
}