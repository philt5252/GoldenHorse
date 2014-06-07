using System;
using Olf.GoldenHorse.Core.Models;
using Olf.GoldenHorse.Foundation.Factories.ViewModels;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.ViewModels;

namespace Olf.GoldenHorse.Core.Factories.ViewModels.Nodes
{
    public class TestItemViewModelFactory : ITestItemViewModelFactory
    {
        private readonly Func<TestItem, ITestItemViewModel> createViewModelFunc;

        public TestItemViewModelFactory(Func<TestItem, ITestItemViewModel> createViewModelFunc)
        {
            this.createViewModelFunc = createViewModelFunc;
        }

        public ITestItemViewModel Create(TestItem testItem)
        {
            return createViewModelFunc(testItem);
        }
    }
}