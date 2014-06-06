
using System;
using Olf.GoldenHorse.Foundation.ViewModels;
using Olf.GoldenHorse.Foundation.Factories.ViewModels;

namespace Olf.GoldenHorse.Core.Factories.ViewModels
{
    public class TestObjectEditorViewModelFactory : ITestObjectEditorViewModelFactory
    {
        private Func<ITestObjectEditorViewModel> createModelFunc;

        public TestObjectEditorViewModelFactory(Func<ITestObjectEditorViewModel> createModelFunc)
        {
            this.createModelFunc = createModelFunc;
        }

        public ITestObjectEditorViewModel Create()
        {
            return createModelFunc();
        }
        
    }
}



