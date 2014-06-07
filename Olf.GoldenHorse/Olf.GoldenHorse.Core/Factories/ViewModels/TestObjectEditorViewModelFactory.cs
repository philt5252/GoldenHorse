
using System;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.ViewModels;
using Olf.GoldenHorse.Foundation.Factories.ViewModels;

namespace Olf.GoldenHorse.Core.Factories.ViewModels
{
    public class TestObjectEditorViewModelFactory : ITestObjectEditorViewModelFactory
    {
        private Func<TestItem, ITestObjectEditorViewModel> createModelFunc;

        public TestObjectEditorViewModelFactory(Func<TestItem, ITestObjectEditorViewModel> createModelFunc)
        {
            this.createModelFunc = createModelFunc;
        }

        public ITestObjectEditorViewModel Create(TestItem testItem)
        {
            return createModelFunc(testItem);
        }
        
    }
}



