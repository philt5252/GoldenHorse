using System;
using System.CodeDom;
using Olf.GoldenHorse.Foundation.Views;
using Olf.GoldenHorse.Foundation.Views.Factories;

namespace Olf.GoldenHorse.Core.Views.Factories
{
    public class TrainingMainWindowFactory : ITrainingMainWindowFactory
    {
        private readonly Func<TrainingMainWindow> createWindowFunc;

        public TrainingMainWindowFactory(Func<TrainingMainWindow> createWindowFunc )
        {
            this.createWindowFunc = createWindowFunc;
        }

        public IWindow Create()
        {
            return createWindowFunc();
        }
    }
}