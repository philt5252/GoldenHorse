using System;
using Olf.GoldenHorse.Foundation.Views;
using Olf.GoldenHorse.Foundation.Views.Factories;

namespace Olf.GoldenHorse.Core.Views.Factories
{
    public class ProjectWorkspaceViewFactory : IProjectWorkspaceViewFactory
    {
        private readonly Func<ProjectWorkspaceView> createView;

        public ProjectWorkspaceViewFactory(Func<ProjectWorkspaceView> createView )
        {
            this.createView = createView;
        }

        public IViewWithDataContext Create()
        {
            return createView();
        }
    }
}