
using System;
using Olf.GoldenHorse.Foundation.ViewModels;
using Olf.GoldenHorse.Foundation.Factories.ViewModels;

namespace Olf.GoldenHorse.Core.Factories.ViewModels
{
    public class TestDescriptionEditorViewModelFactory : ITestDescriptionEditorViewModelFactory
    {
        private Func<ITestDescriptionEditorViewModel> createModelFunc;

        public TestDescriptionEditorViewModelFactory(Func<ITestDescriptionEditorViewModel> createModelFunc)
        {
            this.createModelFunc = createModelFunc;
        }

        public ITestDescriptionEditorViewModel Create()
        {
            return createModelFunc();
        }
        
    }
}



