using System;
using Olf.GoldenHorse.Core.Views.Views;
using Olf.GoldenHorse.Foundation.Views;
using Olf.GoldenHorse.Foundation.Views.Factories;

namespace Olf.GoldenHorse.Core.Views.Factories
{
    public class StartViewFactory : IStartViewFactory
    {
        private readonly Func<StartView> createView;

        public StartViewFactory(Func<StartView> createView)
        {
            this.createView = createView;
        }

        public IViewWithDataContext Create()
        {
            return createView();
        }
    }
}