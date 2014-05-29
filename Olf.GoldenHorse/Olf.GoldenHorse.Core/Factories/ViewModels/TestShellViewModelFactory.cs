
using System;
using Olf.GoldenHorse.Foundation.ViewModels;
using Olf.GoldenHorse.Foundation.Factories.ViewModels;

namespace Olf.GoldenHorse.Core.Factories.ViewModels
{
    public class TestShellViewModelFactory : ITestShellViewModelFactory
    {
        private Func<ITestShellViewModel> createModelFunc;

        public TestShellViewModelFactory(Func<ITestShellViewModel> createModelFunc)
        {
            this.createModelFunc = createModelFunc;
        }

        public ITestShellViewModel Create()
        {
            return createModelFunc();
        }
        
    }
}



