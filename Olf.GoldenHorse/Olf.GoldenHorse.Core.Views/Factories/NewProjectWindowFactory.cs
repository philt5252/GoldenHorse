﻿using System;
using Olf.GoldenHorse.Foundation.Views;
using Olf.GoldenHorse.Foundation.Views.Factories;

namespace Olf.GoldenHorse.Core.Views.Factories
{
    public class NewProjectWindowFactory : INewProjectWindowFactory
    {
        private readonly Func<NewProjectSuiteWindow> createWindow;

        public NewProjectWindowFactory(Func<NewProjectSuiteWindow> createWindow )
        {
            this.createWindow = createWindow;
        }

        public IWindow Create()
        {
            return createWindow();
        }
    }
}