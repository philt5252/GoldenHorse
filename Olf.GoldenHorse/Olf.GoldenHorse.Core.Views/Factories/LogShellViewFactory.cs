using System;
using Olf.GoldenHorse.Core.Views.Views.Logs;
using Olf.GoldenHorse.Foundation.Views;
using Olf.GoldenHorse.Foundation.Views.Factories;

namespace Olf.GoldenHorse.Core.Views.Factories
{
    public class LogShellViewFactory : ILogShellViewFactory
    {
        private readonly Func<LogShellView> createView;

        public LogShellViewFactory(Func<LogShellView> createView)
        {
            this.createView = createView;
        }

        public IViewWithDataContext Create()
        {
            return createView();
        }
    }
}