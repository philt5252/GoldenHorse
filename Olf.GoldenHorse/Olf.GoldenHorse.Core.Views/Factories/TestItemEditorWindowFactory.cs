using System;
using Olf.GoldenHorse.Core.Views.Views.Tests;
using Olf.GoldenHorse.Foundation.Views;
using Olf.GoldenHorse.Foundation.Views.Factories;

namespace Olf.GoldenHorse.Core.Views.Factories
{
    public class TestItemEditorWindowFactory : ITestItemEditorWindowFactory
    {
        private readonly Func<TestItemEditorWindow> createWindow;

        public TestItemEditorWindowFactory(Func<TestItemEditorWindow> createWindow )
        {
            this.createWindow = createWindow;
        }

        public IViewWithDataContext Create()
        {
            return createWindow();
        }
    }
}