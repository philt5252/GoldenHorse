
using System;
using Olf.GoldenHorse.Foundation.ViewModels;
using Olf.GoldenHorse.Foundation.Factories.ViewModels;

namespace Olf.GoldenHorse.Core.Factories.ViewModels
{
    public class TestDetailsViewModelFactory : ITestDetailsViewModelFactory
    {
        private Func<ITestDetailsViewModel> createModelFunc;

        public TestDetailsViewModelFactory(Func<ITestDetailsViewModel> createModelFunc)
        {
            this.createModelFunc = createModelFunc;
        }

        public ITestDetailsViewModel Create()
        {
            return createModelFunc();
        }
        
    }
}



