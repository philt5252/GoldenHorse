using System;
using Olf.GoldenHorse.Core.Views.Views.Logs;
using Olf.GoldenHorse.Foundation.Views;
using Olf.GoldenHorse.Foundation.Views.Factories;

namespace Olf.GoldenHorse.Core.Views.Factories
{
    public class LogMainShellViewFactory : ILogMainShellViewFactory
    {
        private readonly Func<LogMainShellView> createView;

        public LogMainShellViewFactory(Func<LogMainShellView> createView )
        {
            this.createView = createView;
        }

        public IViewWithDataContext Create()
        {
            return createView();
        }
    }
}