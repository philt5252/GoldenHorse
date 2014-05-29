
using System;
using Olf.GoldenHorse.Foundation.ViewModels;
using Olf.GoldenHorse.Foundation.Factories.ViewModels;

namespace Olf.GoldenHorse.Core.Factories.ViewModels
{
    public class NewProjectViewModelFactory : INewProjectViewModelFactory
    {
        private Func<INewProjectViewModel> createModelFunc;

        public NewProjectViewModelFactory(Func<INewProjectViewModel> createModelFunc)
        {
            this.createModelFunc = createModelFunc;
        }

        public INewProjectViewModel Create()
        {
            return createModelFunc();
        }
        
    }
}



