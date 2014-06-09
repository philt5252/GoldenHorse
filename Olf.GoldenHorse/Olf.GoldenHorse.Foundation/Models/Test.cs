using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Xml.Serialization;

namespace Olf.GoldenHorse.Foundation.Models
{
    public class Test
    {
        private ObservableCollection<TestItem> testItems;

        public event EventHandler TestChanged;

        [XmlIgnore]
        public Project Project { get; set; }

        public ObservableCollection<Variable> Variables { get; set; } 

        public string Name { get; set; }

        public ObservableCollection<TestItem> TestItems
        {
            get { return testItems; }
            set
            {
                if (Equals(testItems, value))
                    return;

                if (testItems != null)
                    testItems.CollectionChanged -= TestItemsOnCollectionChanged;

                testItems = value;

                foreach (var testItem in testItems)
                {
                    testItem.Test = this;
                }

                testItems.CollectionChanged += TestItemsOnCollectionChanged;

                RaiseTestChanged();
            }
        }

        public string Id { get; set; }

        public Test()
        {
            TestItems = new ObservableCollection<TestItem>();
            Id = Guid.NewGuid().ToString();
            Variables = new ObservableCollection<Variable>();
        }

        private void TestItemsOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            if (args.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (TestItem testItem in args.NewItems)
                {
                    testItem.Test = this;
                    OnTestChanged();
                }
            }
            else if (args.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (TestItem testItem in args.OldItems)
                {
                    if (testItem.Test == this)
                        testItem.Test = null;

                    RaiseTestChanged();
                }
            }
        }

        public void RaiseTestChanged()
        {
            OnTestChanged();
        }

        protected virtual void OnTestChanged()
        {
            EventHandler handler = TestChanged;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        public void Play(Log log)
        {
            ResetVariables();

            foreach (TestItem testItem in TestItems)
            {
                testItem.Play(log);
            }
        }

        private void ResetVariables()
        {
            foreach (Variable variable in Variables)
            {
                variable.Reset();
            }
        }
    }
}