
using System;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.ViewModels;

namespace Olf.GoldenHorse.Core.ViewModels
{
    public class LogItemViewModel : ILogItemViewModel
    {
        private readonly LogItem logItem;

        public LogItem LogItem { get { return logItem; } }

        public LogItemCategory Category { get { return logItem.Category; } }
        public string Description { get { return logItem.Description; } }
        public string StartTime { get { return logItem.StartTime.ToLongTimeString(); } }
        public bool HasScreenshot { get { return logItem.HasScreenshot; } }

        public LogItemViewModel(LogItem logItem)
        {
            this.logItem = logItem;
        }
    }
}