using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Xml.Serialization;

namespace Olf.GoldenHorse.Foundation.Models
{
    public class Test
    {
        private ObservableCollection<TestItem> testItems;

        [XmlIgnore]
        public Project Project { get; set; }

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
            }
        }

        public Test()
        {
            TestItems = new ObservableCollection<TestItem>();
        }

        private void TestItemsOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            if (args.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (TestItem testItem in args.NewItems)
                {
                    testItem.Test = this;
                }
            }
            else if (args.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (TestItem testItem in args.OldItems)
                {
                    if (testItem.Test == this)
                        testItem.Test = null;
                }
            }
        }
    }
}