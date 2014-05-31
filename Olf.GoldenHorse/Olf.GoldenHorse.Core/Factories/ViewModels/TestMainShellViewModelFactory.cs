
using System;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.ViewModels;
using Olf.GoldenHorse.Foundation.Factories.ViewModels;

namespace Olf.GoldenHorse.Core.Factories.ViewModels
{
    public class TestMainShellViewModelFactory : ITestMainShellViewModelFactory
    {
        private Func<Test, ITestMainShellViewModel> createModelFunc;

        public TestMainShellViewModelFactory(Func<Test, ITestMainShellViewModel> createModelFunc)
        {
            this.createModelFunc = createModelFunc;
        }

        public ITestMainShellViewModel Create(Test test)
        {
            return createModelFunc(test);
        }
        
    }
}



