
using System;
using Olf.GoldenHorse.Foundation.ViewModels;
using Olf.GoldenHorse.Foundation.Factories.ViewModels;

namespace Olf.GoldenHorse.Core.Factories.ViewModels
{
    public class NewProjectSuiteSuiteViewModelFactory : INewProjectSuiteViewModelFactory
    {
        private Func<INewProjectSuiteViewModel> createModelFunc;

        public NewProjectSuiteSuiteViewModelFactory(Func<INewProjectSuiteViewModel> createModelFunc)
        {
            this.createModelFunc = createModelFunc;
        }

        public INewProjectSuiteViewModel Create()
        {
            return createModelFunc();
        }
        
    }
}



