using System;
using Olf.GoldenHorse.Core.Views.Views.Tests.TestEditor;
using Olf.GoldenHorse.Foundation.Views;
using Olf.GoldenHorse.Foundation.Views.Factories;

namespace Olf.GoldenHorse.Core.Views.Factories
{
    public class EditParameterWindowFactory : IEditParameterWindowFactory
    {
        private readonly Func<EditParameterWindow> createWindow;

        public EditParameterWindowFactory(Func<EditParameterWindow> createWindow )
        {
            this.createWindow = createWindow;
        }

        public IWindow Create()
        {
            createWindow();
        }
    }
}