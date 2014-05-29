using System;
using Olf.GoldenHorse.Foundation.Views;
using Olf.GoldenHorse.Foundation.Views.Factories;

namespace Olf.GoldenHorse.Core.Views.Factories
{
    public class TestMainShellViewFactory : ITestMainShellViewFactory
    {
        private readonly Func<TestMainShellView> createView;

        public TestMainShellViewFactory(Func<TestMainShellView> createView )
        {
            this.createView = createView;
        }

        public IViewWithDataContext Create()
        {
            return createView();
        }
    }
}