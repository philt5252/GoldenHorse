
using System;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.ViewModels;
using Olf.GoldenHorse.Foundation.Factories.ViewModels;

namespace Olf.GoldenHorse.Core.Factories.ViewModels
{
    public class LogShellViewModelFactory : ILogShellViewModelFactory
    {
        private Func<Log, ILogShellViewModel> createModelFunc;

        public LogShellViewModelFactory(Func<Log, ILogShellViewModel> createModelFunc)
        {
            this.createModelFunc = createModelFunc;
        }

        public ILogShellViewModel Create(Log log)
        {
            return createModelFunc(log);
        }
        
    }
}



