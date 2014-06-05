
using System;
using Olf.GoldenHorse.Foundation.ViewModels;
using Olf.GoldenHorse.Foundation.Factories.ViewModels;

namespace Olf.GoldenHorse.Core.Factories.ViewModels
{
    public class StartPageViewModelFactory : IStartPageViewModelFactory
    {
        private Func<IStartPageViewModel> createModelFunc;

        public StartPageViewModelFactory(Func<IStartPageViewModel> createModelFunc)
        {
            this.createModelFunc = createModelFunc;
        }

        public IStartPageViewModel Create()
        {
            return createModelFunc();
        }
        
    }
}



