
using System;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.ViewModels;
using Olf.GoldenHorse.Foundation.Factories.ViewModels;

namespace Olf.GoldenHorse.Core.Factories.ViewModels
{
    public class LogDetailsViewModelFactory : ILogDetailsViewModelFactory
    {
        private Func<Log, ILogDetailsViewModel> createModelFunc;

        public LogDetailsViewModelFactory(Func<Log, ILogDetailsViewModel> createModelFunc)
        {
            this.createModelFunc = createModelFunc;
        }

        public ILogDetailsViewModel Create(Log log)
        {
            return createModelFunc(log);
        }
        
    }
}



