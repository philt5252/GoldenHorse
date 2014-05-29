using System;
using Olf.GoldenHorse.Foundation.Views;
using Olf.GoldenHorse.Foundation.Views.Factories;

namespace Olf.GoldenHorse.Core.Views.Factories
{
    public class MainShellViewFactory : IMainShellViewFactory
    {
        private readonly Func<MainShellView> createView;

        public MainShellViewFactory(Func<MainShellView> createView)
        {
            this.createView = createView;
        }

        public IViewWithDataContext Create()
        {
            return createView();
        }
    }
}