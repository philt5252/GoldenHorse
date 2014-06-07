
using System;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.ViewModels;
using Olf.GoldenHorse.Foundation.Factories.ViewModels;

namespace Olf.GoldenHorse.Core.Factories.ViewModels
{
    public class TestItemEditorViewModelFactory : ITestItemEditorViewModelFactory
    {
        private Func<TestItem,ITestItemEditorViewModel> createModelFunc;

        public TestItemEditorViewModelFactory(Func<TestItem, ITestItemEditorViewModel> createModelFunc)
        {
            this.createModelFunc = createModelFunc;
        }

        public ITestItemEditorViewModel Create(TestItem testItem)
        {
            return createModelFunc(testItem);
        }
        
    }
}



