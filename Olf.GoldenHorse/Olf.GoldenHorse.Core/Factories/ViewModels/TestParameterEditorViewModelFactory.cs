
using System;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.ViewModels;
using Olf.GoldenHorse.Foundation.Factories.ViewModels;

namespace Olf.GoldenHorse.Core.Factories.ViewModels
{
    public class TestParameterEditorViewModelFactory : ITestParameterEditorViewModelFactory
    {
        private Func<TestItem, ITestParameterEditorViewModel> createModelFunc;

        public TestParameterEditorViewModelFactory(Func<TestItem, ITestParameterEditorViewModel> createModelFunc)
        {
            this.createModelFunc = createModelFunc;
        }

        public ITestParameterEditorViewModel Create(TestItem testItem)
        {
            return createModelFunc(testItem);
        }
        
    }
}



