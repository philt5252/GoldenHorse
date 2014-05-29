using System;
using Olf.GoldenHorse.Foundation.Views;
using Olf.GoldenHorse.Foundation.Views.Factories;

namespace Olf.GoldenHorse.Core.Views.Factories
{
    public class RecordWindowFactory : IRecordWindowFactory
    {
        private readonly Func<RecordWindow> createWindow;

        public RecordWindowFactory(Func<RecordWindow> createWindow )
        {
            this.createWindow = createWindow;
        }

        public IWindow Create()
        {
            return createWindow();
        }
    }
}