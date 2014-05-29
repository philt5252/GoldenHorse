
using System;
using Olf.GoldenHorse.Foundation.ViewModels;
using Olf.GoldenHorse.Foundation.Factories.ViewModels;

namespace Olf.GoldenHorse.Core.Factories.ViewModels
{
    public class TestOperationsViewModelFactory : ITestOperationsViewModelFactory
    {
        private Func<ITestOperationsViewModel> createModelFunc;

        public TestOperationsViewModelFactory(Func<ITestOperationsViewModel> createModelFunc)
        {
            this.createModelFunc = createModelFunc;
        }

        public ITestOperationsViewModel Create()
        {
            return createModelFunc();
        }
        
    }
}



