
using System;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.ViewModels;
using Olf.GoldenHorse.Foundation.Factories.ViewModels;

namespace Olf.GoldenHorse.Core.Factories.ViewModels
{
    public class TestDetailsViewModelFactory : ITestDetailsViewModelFactory
    {
        private Func<Test, ITestDetailsViewModel> createModelFunc;

        public TestDetailsViewModelFactory(Func<Test, ITestDetailsViewModel> createModelFunc)
        {
            this.createModelFunc = createModelFunc;
        }

        public ITestDetailsViewModel Create(Test test)
        {
            return createModelFunc(test);
        }
        
    }
}



