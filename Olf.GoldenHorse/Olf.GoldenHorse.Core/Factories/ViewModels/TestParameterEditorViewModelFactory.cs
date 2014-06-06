
using System;
using Olf.GoldenHorse.Foundation.ViewModels;
using Olf.GoldenHorse.Foundation.Factories.ViewModels;

namespace Olf.GoldenHorse.Core.Factories.ViewModels
{
    public class TestParameterEditorViewModelFactory : ITestParameterEditorViewModelFactory
    {
        private Func<ITestParameterEditorViewModel> createModelFunc;

        public TestParameterEditorViewModelFactory(Func<ITestParameterEditorViewModel> createModelFunc)
        {
            this.createModelFunc = createModelFunc;
        }

        public ITestParameterEditorViewModel Create()
        {
            return createModelFunc();
        }
        
    }
}



