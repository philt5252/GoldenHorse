
using System;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.ViewModels;
using Olf.GoldenHorse.Foundation.Factories.ViewModels;

namespace Olf.GoldenHorse.Core.Factories.ViewModels
{
    public class TestScreenshotsViewModelFactory : ITestScreenshotsViewModelFactory
    {
        private Func<Test, ITestScreenshotsViewModel> createModelFunc;

        public TestScreenshotsViewModelFactory(Func<Test, ITestScreenshotsViewModel> createModelFunc)
        {
            this.createModelFunc = createModelFunc;
        }

        public ITestScreenshotsViewModel Create(Test test)
        {
            return createModelFunc(test);
        }
        
    }
}



