using System;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.ViewModels;
using Olf.GoldenHorse.Foundation.Factories.ViewModels;

namespace Olf.GoldenHorse.Core.Factories.ViewModels
{
    public class TestShellViewModelFactory : ITestShellViewModelFactory
    {
        private Func<Test, ITestShellViewModel> createModelFunc;

        public TestShellViewModelFactory(Func<Test, ITestShellViewModel> createModelFunc)
        {
            this.createModelFunc = createModelFunc;
        }

        public ITestShellViewModel Create(Test test)
        {
            return createModelFunc(test);
        }
        
    }
}



