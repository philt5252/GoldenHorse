using System;
using Olf.GoldenHorse.Foundation.Views;
using Olf.GoldenHorseTraining.Foundation.Views.Factories;

namespace Olf.GoldenHorseTraining.Core.Views.Factories
{
    public class MainWindowFactory : IMainWindowFactory
    {
        private readonly Func<MainWindow> createWindowFunc;

        public MainWindowFactory(Func<MainWindow> createWindowFunc )
        {
            this.createWindowFunc = createWindowFunc;
        }

        public IWindow Create()
        {
            return createWindowFunc();
        }
    }
}