using System;
using Olf.GoldenHorse.Foundation.Views;
using Olf.GoldenHorse.Foundation.Views.Factories;

namespace Olf.GoldenHorse.Core.Views.Factories
{
    public class WorkspaceViewFactory : IWorkspaceViewFactory
    {
        private readonly Func<WorkspaceView> createView;

        public WorkspaceViewFactory(Func<WorkspaceView> createView )
        {
            this.createView = createView;
        }

        public IViewWithDataContext Create()
        {
            return createView();
        }
    }
}