
using System;
using Olf.GoldenHorse.Foundation.ViewModels;
using Olf.GoldenHorse.Foundation.Factories.ViewModels;

namespace Olf.GoldenHorse.Core.Factories.ViewModels
{
    public class TestOperationEditorViewModelFactory : ITestOperationEditorViewModelFactory
    {
        private Func<ITestOperationEditorViewModel> createModelFunc;

        public TestOperationEditorViewModelFactory(Func<ITestOperationEditorViewModel> createModelFunc)
        {
            this.createModelFunc = createModelFunc;
        }

        public ITestOperationEditorViewModel Create()
        {
            return createModelFunc();
        }
        
    }
}



