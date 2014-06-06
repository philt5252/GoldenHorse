
using System;
using Olf.GoldenHorse.Foundation.ViewModels;
using Olf.GoldenHorse.Foundation.Factories.ViewModels;

namespace Olf.GoldenHorse.Core.Factories.ViewModels
{
    public class TestItemEditorViewModelFactory : ITestItemEditorViewModelFactory
    {
        private Func<ITestItemEditorViewModel> createModelFunc;

        public TestItemEditorViewModelFactory(Func<ITestItemEditorViewModel> createModelFunc)
        {
            this.createModelFunc = createModelFunc;
        }

        public ITestItemEditorViewModel Create()
        {
            return createModelFunc();
        }
        
    }
}



