
using System;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.ViewModels;
using Olf.GoldenHorse.Foundation.Factories.ViewModels;

namespace Olf.GoldenHorse.Core.Factories.ViewModels
{
    public class LogItemViewModelFactory : ILogItemViewModelFactory
    {
        private Func<LogItem, ILogItemViewModel> createModelFunc;

        public LogItemViewModelFactory(Func<LogItem, ILogItemViewModel> createModelFunc)
        {
            this.createModelFunc = createModelFunc;
        }

        public ILogItemViewModel Create(LogItem logItem)
        {
            return createModelFunc(logItem);
        }
        
    }
}



