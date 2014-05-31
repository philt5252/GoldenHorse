using System;
using Olf.GoldenHorse.Foundation.Views;
using Olf.GoldenHorse.Foundation.Views.Factories;

namespace Olf.GoldenHorse.Core.Views.Factories
{
    public class TestWorkspaceViewFactory : ITestWorkspaceViewFactory
    {
        private readonly Func<TestWorkspaceView> createView;

        public TestWorkspaceViewFactory(Func<TestWorkspaceView> createView )
        {
            this.createView = createView;
        }

        public IViewWithDataContext Create()
        {
            return createView();
        }
    }
}