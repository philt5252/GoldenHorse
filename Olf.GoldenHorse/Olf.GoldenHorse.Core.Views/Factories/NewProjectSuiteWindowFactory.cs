using System;
using Olf.GoldenHorse.Foundation.Views;
using Olf.GoldenHorse.Foundation.Views.Factories;

namespace Olf.GoldenHorse.Core.Views.Factories
{
    public class NewProjectSuiteWindowFactory : INewProjectSuiteWindowFactory
    {
        private readonly Func<NewProjectSuiteWindow> createWindow;

        public NewProjectSuiteWindowFactory(Func<NewProjectSuiteWindow> createWindow )
        {
            this.createWindow = createWindow;
        }

        public IWindow Create()
        {
            return createWindow();
        }
    }
}