
using System;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.ViewModels;
using Olf.GoldenHorse.Foundation.Factories.ViewModels;

namespace Olf.GoldenHorse.Core.Factories.ViewModels
{
    public class TrainingMainViewModelFactory : ITrainingMainViewModelFactory
    {
        private Func<Test, ITrainingMainViewModel> createModelFunc;

        public TrainingMainViewModelFactory(Func<Test, ITrainingMainViewModel> createModelFunc)
        {
            this.createModelFunc = createModelFunc;
        }

        public ITrainingMainViewModel Create(Test test)
        {
            return createModelFunc(test);
        }
        
    }
}



