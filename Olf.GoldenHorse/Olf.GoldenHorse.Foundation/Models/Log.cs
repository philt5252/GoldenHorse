using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Xml.Serialization;

namespace Olf.GoldenHorse.Foundation.Models
{
    public class Log
    {
        public event EventHandler NameChanged;

        public string Name
        {
            get { return name; }
            set
            {
                name = value; 
                OnNameChanged();
            }
        }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public ObservableCollection<LogItem> LogItems
        {
            get { return logItems; }
            set
            {
                if (logItems != null)
                    logItems.CollectionChanged -= LogItemsOnCollectionChanged;

                logItems = value; 
                
                logItems.CollectionChanged += LogItemsOnCollectionChanged;
            }
        }

        private IList<LogItem> currentLogItems;
        private Stack<IList<LogItem>> logItemsStack = new Stack<IList<LogItem>>();
        
        [XmlIgnore]
        public ILogOwner Owner { get; set; }

        private LogItem currentLogItem;
        private ObservableCollection<LogItem> logItems;
        private string name;

        public Log()
        {
            LogItems = new ObservableCollection<LogItem>();
            currentLogItems = LogItems;
        }

        private void LogItemsOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            if (args.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (LogItem newItem in args.NewItems)
                {
                    newItem.Log = this;
                }
            }
        }

        public void StartLogItemChildren()
        {
            logItemsStack.Push(currentLogItems);
            currentLogItems = currentLogItems.Last().Children;
        }

        public void EndLogItemChildren()
        {
            currentLogItems = logItemsStack.Pop();
        }

        protected virtual void OnNameChanged()
        {
            EventHandler handler = NameChanged;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        public void CreateLogItem(LogItemCategory logItemCategory, string description, Screenshot screenshot = null)
        {
            LogItem logItem = new LogItem();
            logItem.StartTime = DateTime.Now;
            logItem.Description = description;
            logItem.Category = logItemCategory;
            logItem.Screenshot = screenshot;

            currentLogItems.Add(logItem);
        }
    }
}