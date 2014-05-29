using System;
using Olf.GoldenHorse.Foundation.ViewModels;
using Olf.GoldenHorse.Foundation.Factories.ViewModels;

namespace Olf.GoldenHorse.Core.Factories.ViewModels
{
    public class ProjectExplorerViewModelFactory : IProjectExplorerViewModelFactory
    {
        private Func<IProjectExplorerViewModel> createModelFunc;

        public ProjectExplorerViewModelFactory(Func<IProjectExplorerViewModel> createModelFunc)
        {
            this.createModelFunc = createModelFunc;
        }

        public IProjectExplorerViewModel Create()
        {
            return createModelFunc();
        }
        
    }
}



