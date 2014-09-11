using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Xml.Serialization;
using Olf.GoldenHorse.Foundation.Services;

namespace Olf.GoldenHorse.Foundation.Models
{
    public class TestItem : ScreenshotOwner, ITestItemOwner
    {
        private ObservableCollection<TestItem> testItems;
        private MappedItem control;
        private string description;
        private Operation operation;
        private string controlId;
        private string type;
        private Test test;

        public event EventHandler OperationChanged;

        public event EventHandler ParametersChanged;

        public event EventHandler DescriptionChanged;

        public event EventHandler ControlChanged;

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
                OnOperationChanged();
                RaiseTestChanged();
            }
        }

        private void OperationOnParameterValuesChanged(object sender, EventArgs eventArgs)
        {
            OnDescriptionChanged();
            OnParametersChanged();
            RaiseTestChanged();
        }

        public string ControlId
        {
            get { return controlId; }
            set
            {
                controlId = value;
                control = null;
                OnControlChanged();
                RaiseTestChanged();
            }
        }

        [XmlIgnore]
        public MappedItem Control { get { return control ?? (control = AppManager.GetMappedItem(ControlId)); } }

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
        public Test Test
        {
            get { return test; }
            set
            {
                test = value;

                if (TestItems != null)
                {
                    foreach (TestItem testItem in TestItems)
                    {
                        testItem.Test = test;
                    }
                }
            }
        }

        [XmlIgnore]
        public AppManager AppManager {get { return Test.Project.AppManager; } }

        [XmlIgnore]
        public ITestItemOwner Parent { get; set; }

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
                    testItem.Test = Test;
                    testItem.Parent = this;
                }

                testItems.CollectionChanged += TestItemsOnCollectionChanged;
                RaiseTestChanged();
            }
        }

        public string DescriptionOverride { get; set; }

        [XmlIgnore]
        public virtual string Description
        {
            get { return DescriptionOverride ?? DefaultDescription(); }
        }

        public bool SupportsChildren { get; set; }

        public TestItem()
        {
            TestItems = new ObservableCollection<TestItem>();
            Id = Guid.NewGuid().ToString();
            SupportsChildren = false;
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

        private void TestItemsOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            if (args.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (TestItem testItem in args.NewItems)
                {
                    testItem.Test = Test;
                    testItem.Parent = this;
                }
            }
            else if (args.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (TestItem testItem in args.OldItems)
                {
                    if (testItem.Test == Test)
                        testItem.Test = null;

                    if (testItem.Parent == this)
                        testItem.Parent = null;
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

        protected virtual void OnParametersChanged()
        {
            EventHandler handler = ParametersChanged;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        protected virtual void OnOperationChanged()
        {
            EventHandler handler = OperationChanged;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        protected virtual void OnControlChanged()
        {
            EventHandler handler = ControlChanged;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        public virtual bool Play(Log log)
        {
            try
            {
                bool result; 
                result = Operation.Play(Control, log);
                return result;
            }
            catch (Exception ex)
            {
                log.CreateLogItem(LogItemCategory.Error, string.Format("An error occurred when executing the {0} Operation.", Operation.Name), null);
                return false;
            }
        }
    }

    public interface ITestItemOwner
    {
        ObservableCollection<TestItem> TestItems { get; set; }
    }
}