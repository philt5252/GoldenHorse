using System;
using Olf.GoldenHorse.Foundation.ViewModels;
using Olf.GoldenHorse.Foundation.Factories.ViewModels;

namespace Olf.GoldenHorse.Core.Factories.ViewModels
{
    public class MainShellViewModelFactory : IMainShellViewModelFactory
    {
        private Func<IMainShellViewModel> createModelFunc;

        public MainShellViewModelFactory(Func<IMainShellViewModel> createModelFunc)
        {
            this.createModelFunc = createModelFunc;
        }

        public IMainShellViewModel Create()
        {
            return createModelFunc();
        }
        
    }
}



