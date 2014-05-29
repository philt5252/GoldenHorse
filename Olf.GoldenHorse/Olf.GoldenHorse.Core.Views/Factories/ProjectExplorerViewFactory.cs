using System;
using Olf.GoldenHorse.Foundation.Views;
using Olf.GoldenHorse.Foundation.Views.Factories;

namespace Olf.GoldenHorse.Core.Views.Factories
{
    public class ProjectExplorerViewFactory : IProjectExplorerViewFactory
    {
        private readonly Func<ProjectExplorerView> createView;

        public ProjectExplorerViewFactory(Func<ProjectExplorerView> createView )
        {
            this.createView = createView;
        }

        public IViewWithDataContext Create()
        {
            return createView();
        }
    }
}