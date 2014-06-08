using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Xml.Serialization;
using Olf.GoldenHorse.Foundation.Services;

namespace Olf.GoldenHorse.Foundation.Models
{
    public class TestItem : ScreenshotOwner
    {
        private ObservableCollection<TestItem> children;
        private MappedItem control;

        private string description;

        private Operation operation;
        public Operation Operation
        {
            get { return operation; }
            set
            {
                operation = value;
                operation.TestItem = this;
            }
        }

        public string ControlId { get; set; }

        [XmlIgnore]
        public MappedItem Control { get { return control ?? (control = AppManager.GetMappedItem(ControlId)); } }

        public string Id { get; set; }

        public string Type { get; set; }

        [XmlIgnore]
        public Test Test { get; set; }

        [XmlIgnore]
        public AppManager AppManager {get { return Test.Project.AppManager; } }

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

        public virtual string Description
        {
            get { return description ?? DefaultDescription(); }
            set { description = value; }
        }

        public TestItem()
        {
            Children = new ObservableCollection<TestItem>();
            Id = Guid.NewGuid().ToString();
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

        protected virtual string DefaultDescription()
        {
            return Operation == null ? "" : Operation.DefaultDescription(Control);
        }

        public virtual void Play(Log log)
        {
            Operation.Play(Control, log);
        }
    }
}