using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Xml.Serialization;

namespace Olf.GoldenHorse.Foundation.Models
{
    public class Test : ITestItemOwner
    {
        private ObservableCollection<TestItem> testItems;
        private string name;

        public event EventHandler TestChanged;
        public event EventHandler NameChanged;

        [XmlIgnore]
        public Project Project { get; set; }

        public ObservableCollection<Variable> Variables { get; set; }

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnNameChanged();
                //RaiseTestChanged();
            }
        }

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
                    testItem.Parent = this;
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
                    testItem.Parent = this;
                    OnTestChanged();
                }
            }
            else if (args.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (TestItem testItem in args.OldItems)
                {
                    if (testItem.Test == this)
                        testItem.Test = null;

                    if (testItem.Parent == this)
                        testItem.Parent = null;

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

        protected virtual void OnNameChanged()
        {
            EventHandler handler = NameChanged;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        public void Play(Log log)
        {
            ResetVariables();

            foreach (TestItem testItem in TestItems)
            {
                if (!testItem.Play(log))
                    break;
            }
        }

        private void ResetVariables()
        {
            foreach (Variable variable in Variables)
            {
                variable.Reset();
            }
        }

        public void Play(Log log, string id)
        {
            ResetVariables();

            foreach (TestItem testItem in TestItems.SkipWhile(t => !Equals(t.Id, id)))
            {
                testItem.Play(log);
            }
        }
    }
}