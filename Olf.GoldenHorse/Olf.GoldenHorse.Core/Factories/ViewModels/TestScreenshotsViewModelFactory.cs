
using System;
using Olf.GoldenHorse.Foundation.ViewModels;
using Olf.GoldenHorse.Foundation.Factories.ViewModels;

namespace Olf.GoldenHorse.Core.Factories.ViewModels
{
    public class TestScreenshotsViewModelFactory : ITestScreenshotsViewModelFactory
    {
        private Func<ITestScreenshotsViewModel> createModelFunc;

        public TestScreenshotsViewModelFactory(Func<ITestScreenshotsViewModel> createModelFunc)
        {
            this.createModelFunc = createModelFunc;
        }

        public ITestScreenshotsViewModel Create()
        {
            return createModelFunc();
        }
        
    }
}



