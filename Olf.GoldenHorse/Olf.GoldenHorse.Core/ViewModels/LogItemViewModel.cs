
using System;
using System.Collections.Generic;
using System.Linq;
using Olf.GoldenHorse.Foundation.Factories.ViewModels;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.ViewModels;

namespace Olf.GoldenHorse.Core.ViewModels
{
    public class LogItemViewModel : ILogItemViewModel
    {
        private readonly LogItem logItem;
        private readonly ILogItemViewModelFactory logItemViewModelFactory;

        public LogItem LogItem { get { return logItem; } }

        public IList<ILogItemViewModel> Children { get; protected set; }

        public LogItemCategory Category { get { return logItem.Category; } }
        public string Description { get { return logItem.Description; } }
        public string StartTime { get { return logItem.StartTime.ToLongTimeString(); } }
        public bool HasScreenshot { get { return logItem.HasScreenshot; } }

        public LogItemViewModel(LogItem logItem, ILogItemViewModelFactory logItemViewModelFactory)
        {
            this.logItem = logItem;
            this.logItemViewModelFactory = logItemViewModelFactory;
            Children = logItem.Children.Select(logItemViewModelFactory.Create).ToList();
        }
    }
}