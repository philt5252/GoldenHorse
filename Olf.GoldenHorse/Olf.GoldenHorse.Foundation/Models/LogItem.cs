using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Xml.Serialization;
using Olf.GoldenHorse.Foundation.Services;

namespace Olf.GoldenHorse.Foundation.Models
{
    public class LogItem : ScreenshotOwner
    {
        private ObservableCollection<LogItem> children;
        private Log log;

        [XmlIgnore]
        public Log Log
        {
            get { return log; }
            set
            {
                log = value;

                foreach (LogItem logItem in Children)
                {
                    logItem.Log = log;
                }
            }
        }

        public DateTime StartTime { get; set; }
        public LogItemCategory Category { get; set; }
        public string Description { get; set; }
        public ObservableCollection<LogItem> Children
        {
            get { return children; }
            set
            {
                if (children != null)
                    children.CollectionChanged -= ChildrenOnCollectionChanged;

                children = value;

                children.CollectionChanged += ChildrenOnCollectionChanged;
            }
        }

        public LogItem()
        {
            Children = new ObservableCollection<LogItem>();
        }

        private void ChildrenOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            if (args.Action == NotifyCollectionChangedAction.Add && Log != null)
            {
                foreach (LogItem newItem in args.NewItems)
                {
                    newItem.Log = Log;
                }
            }
        }

        public override string GetScreenshotFolder()
        {
            return ProjectSuiteManager.GetScreenshotsFolder(this.Log);
        }
    }
}