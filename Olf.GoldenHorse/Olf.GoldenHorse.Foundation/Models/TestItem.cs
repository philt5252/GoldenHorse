using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Xml.Serialization;
using Olf.GoldenHorse.Foundation.Services;

namespace Olf.GoldenHorse.Foundation.Models
{
    public abstract class TestItem : ScreenshotOwner
    {
        private ObservableCollection<TestItem> children;

        [XmlIgnore]
        public Test Test { get; set; }

        public ObservableCollection<TestItem> Children
        {
            get { return children; }
            set
            {
                if (Equals(children, value))
                    return;

                if (children != null)
                    children.CollectionChanged -= ChildrenOnCollectionChanged;

                children = value;

                foreach (var testItem in children)
                {
                    testItem.Test = Test;
                }

                children.CollectionChanged += ChildrenOnCollectionChanged;
            }
        }

        public virtual string Description { get; set; }

        protected TestItem()
        {
            Children = new ObservableCollection<TestItem>();
        }

        public override string GetScreenshotFolder()
        {
            return ProjectManager.GetScreenshotsFolder(Test);
        }

        private void ChildrenOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            if (args.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (TestItem testItem in args.NewItems)
                {
                    testItem.Test = Test;
                }
            }
            else if (args.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (TestItem testItem in args.OldItems)
                {
                    if (testItem.Test == Test)
                        testItem.Test = null;
                }
            }
        }

        public abstract void Play();
    }
}