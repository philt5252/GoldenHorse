using System;
using Olf.GoldenHorse.Core.Models;
using Olf.GoldenHorse.Foundation.Factories.ViewModels;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.ViewModels;

namespace Olf.GoldenHorse.Core.Factories.ViewModels.Nodes
{
    public class TestItemViewModelFactory : ITestItemViewModelFactory
    {
        private readonly Func<OnScreenAction, IOnScreenActionViewModel> createOnScreenActionViewModelFunc;

        public TestItemViewModelFactory(Func<OnScreenAction, IOnScreenActionViewModel> createOnScreenActionViewModelFunc)
        {
            this.createOnScreenActionViewModelFunc = createOnScreenActionViewModelFunc;
        }

        public ITestItemViewModel Create(TestItem testItem)
        {
            if (testItem is OnScreenAction)
                return createOnScreenActionViewModelFunc(testItem as OnScreenAction);

            throw new Exception();
        }
    }
}