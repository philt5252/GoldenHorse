
using System;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.ViewModels;
using Olf.GoldenHorse.Foundation.Factories.ViewModels;

namespace Olf.GoldenHorse.Core.Factories.ViewModels
{
    public class LogScreenshotsViewModelFactory : ILogScreenshotsViewModelFactory
    {
        private Func<Log, ILogScreenshotsViewModel> createModelFunc;

        public LogScreenshotsViewModelFactory(Func<Log, ILogScreenshotsViewModel> createModelFunc)
        {
            this.createModelFunc = createModelFunc;
        }

        public ILogScreenshotsViewModel Create(Log log)
        {
            return createModelFunc(log);
        }
        
    }
}



