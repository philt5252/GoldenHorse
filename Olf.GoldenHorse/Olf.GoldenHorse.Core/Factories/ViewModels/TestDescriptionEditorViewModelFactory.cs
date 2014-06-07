
using System;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.ViewModels;
using Olf.GoldenHorse.Foundation.Factories.ViewModels;

namespace Olf.GoldenHorse.Core.Factories.ViewModels
{
    public class TestDescriptionEditorViewModelFactory : ITestDescriptionEditorViewModelFactory
    {
        private Func<TestItem, ITestDescriptionEditorViewModel> createModelFunc;

        public TestDescriptionEditorViewModelFactory(Func<TestItem, ITestDescriptionEditorViewModel> createModelFunc)
        {
            this.createModelFunc = createModelFunc;
        }

        public ITestDescriptionEditorViewModel Create(TestItem testItem)
        {
            return createModelFunc(testItem);
        }
        
    }
}



