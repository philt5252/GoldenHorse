using System;
using Olf.GoldenHorse.Foundation.Views;
using Olf.GoldenHorse.Foundation.Views.Factories;

namespace Olf.GoldenHorse.Core.Views.Factories
{
    public class MainWindowFactory : IMainWindowFactory
    {
        private readonly Func<MainWindow> createWindow;

        public MainWindowFactory(Func<MainWindow> createWindow)
        {
            this.createWindow = createWindow;
        }

        public IWindow Create()
        {
            return createWindow();
        }
    }
}