using System;
using Olf.GoldenHorse.Foundation.Views;
using Olf.GoldenHorse.Foundation.Views.Factories;

namespace Olf.GoldenHorse.Core.Views.Factories
{
    public class TestShellViewFactory : ITestShellViewFactory
    {
        private readonly Func<TestShellView> createView;

        public TestShellViewFactory(Func<TestShellView> createView )
        {
            this.createView = createView;
        }

        public IViewWithDataContext Create()
        {
            return createView();
        }
    }
}