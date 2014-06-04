
using System;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.ViewModels;
using Olf.GoldenHorse.Foundation.Factories.ViewModels;

namespace Olf.GoldenHorse.Core.Factories.ViewModels
{
    public class LogMainShellViewModelFactory : ILogMainShellViewModelFactory
    {
        private Func<Log, ILogMainShellViewModel> createModelFunc;

        public LogMainShellViewModelFactory(Func<Log, ILogMainShellViewModel> createModelFunc)
        {
            this.createModelFunc = createModelFunc;
        }

        public ILogMainShellViewModel Create(Log log)
        {
            return createModelFunc(log);
        }
        
    }
}



