
using System;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.ViewModels;
using Olf.GoldenHorse.Foundation.Factories.ViewModels;

namespace Olf.GoldenHorse.Core.Factories.ViewModels
{
    public class TestOperationsViewModelFactory : ITestOperationsViewModelFactory
    {
        private Func<Test, ITestOperationsViewModel> createModelFunc;

        public TestOperationsViewModelFactory(Func<Test, ITestOperationsViewModel> createModelFunc)
        {
            this.createModelFunc = createModelFunc;
        }

        public ITestOperationsViewModel Create(Test test)
        {
            return createModelFunc(test);
        }
        
    }
}



