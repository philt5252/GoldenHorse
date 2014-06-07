
using System;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.ViewModels;
using Olf.GoldenHorse.Foundation.Factories.ViewModels;

namespace Olf.GoldenHorse.Core.Factories.ViewModels
{
    public class TestOperationEditorViewModelFactory : ITestOperationEditorViewModelFactory
    {
        private Func<TestItem, ITestOperationEditorViewModel> createModelFunc;

        public TestOperationEditorViewModelFactory(Func<TestItem, ITestOperationEditorViewModel> createModelFunc)
        {
            this.createModelFunc = createModelFunc;
        }

        public ITestOperationEditorViewModel Create(TestItem testItem)
        {
            return createModelFunc(testItem);
        }
        
    }
}



