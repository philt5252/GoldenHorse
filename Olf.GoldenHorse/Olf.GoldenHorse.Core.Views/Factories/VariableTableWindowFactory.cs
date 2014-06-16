using System;
using Olf.GoldenHorse.Core.Views.Views.Variables;
using Olf.GoldenHorse.Foundation.Views;
using Olf.GoldenHorse.Foundation.Views.Factories;

namespace Olf.GoldenHorse.Core.Views.Factories
{
    public class VariableTableWindowFactory : IVariableTableWindowFactory
    {
        private readonly Func<VariableTableWindow> createWindow;

        public VariableTableWindowFactory(Func<VariableTableWindow> createWindow )
        {
            this.createWindow = createWindow;
        }

        public IWindow Create()
        {
            return createWindow();
        }
    }
}