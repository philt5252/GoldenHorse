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
        private string controlId;
        private string type;

        public event EventHandler DescriptionChanged;

        public Operation Operation
        {
            get { return operation; }
            set
            {
                if (operation != null)
                {
                    operation.ParameterValuesChanged -= OperationOnParameterValuesChanged;
                }

                operation = value;
                operation.TestItem = this;

                Operation.ParameterValuesChanged += OperationOnParameterValuesChanged;

                RaiseTestChanged();
            }
        }

        private void OperationOnParameterValuesChanged(object sender, EventArgs eventArgs)
        {
            OnDescriptionChanged();
            RaiseTestChanged();
        }

        public string ControlId
        {
            get { return controlId; }
            set
            {
                controlId = value;
                RaiseTestChanged();
            }
        }

        [XmlIgnore]
        public MappedItem Control { get { return control ?? (control = AppManager.GetMappedItem(ControlId)); } }

        public string Id { get; set; }

        public string Type
        {
            get { return type; }
            set
            {
                type = value;
                RaiseTestChanged();
            }
        }

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
                RaiseTestChanged();
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

        private void RaiseTestChanged()
        {
            if(Test != null)
                Test.RaiseTestChanged();
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

            RaiseTestChanged();
        }

        protected virtual string DefaultDescription()
        {
            return Operation == null ? "" : Operation.DefaultDescription(Control);
        }

        protected virtual void OnDescriptionChanged()
        {
            EventHandler handler = DescriptionChanged;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        public virtual void Play(Log log)
        {
            Operation.Play(Control, log);
        }
    }
}