
using System;
using Olf.GoldenHorse.Foundation.ViewModels;
using Olf.GoldenHorse.Foundation.Factories.ViewModels;

namespace Olf.GoldenHorse.Core.Factories.ViewModels
{
    public class TestMainShellViewModelFactory : ITestMainShellViewModelFactory
    {
        private Func<ITestMainShellViewModel> createModelFunc;

        public TestMainShellViewModelFactory(Func<ITestMainShellViewModel> createModelFunc)
        {
            this.createModelFunc = createModelFunc;
        }

        public ITestMainShellViewModel Create()
        {
            return createModelFunc();
        }
        
    }
}



