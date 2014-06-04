using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace Olf.GoldenHorse.Foundation.Models
{
    public class Log
    {
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

        

        public string Folder { get; set; }

        private LogItem currentLogItem;
        private ObservableCollection<LogItem> logItems;

        public Log()
        {
            LogItems = new ObservableCollection<LogItem>();
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

        public void CreateLogItem()
        {
            
        }

        public void StartLogItemChildren()
        {
            
        }

        public void EndLogItemChildren()
        {
            
        }

        public void CreateLogItem(LogItemCategory logItemCategory, string description, Screenshot screenshot = null)
        {
            LogItem logItem = new LogItem();
            logItem.StartTime = DateTime.Now;
            logItem.Description = description;
            logItem.Category = logItemCategory;
            logItem.Screenshot = screenshot;

            LogItems.Add(logItem);
        }
    }
}