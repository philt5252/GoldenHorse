using System;
using Olf.GoldenHorse.Core.Views.Views.Tests.TestEditor;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.Views;
using Olf.GoldenHorse.Foundation.Views.Factories;

namespace Olf.GoldenHorse.Core.Views.Factories
{
    public class EditParameterWindowFactory : IEditParameterWindowFactory
    {
        private readonly Func<EditParameterWindow> createWindow;
        private readonly Func<EditImageParameterWindow> createImageParamWindow;

        public EditParameterWindowFactory(Func<EditParameterWindow> createWindow, 
            Func<EditImageParameterWindow> createImageParamWindow)
        {
            this.createWindow = createWindow;
            this.createImageParamWindow = createImageParamWindow;
        }

        public IWindow Create(string paramType)
        {
            if(paramType != "Image")
                return createWindow();

            return createImageParamWindow();
        }
    }
}