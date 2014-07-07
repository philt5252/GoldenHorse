
using System;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.ViewModels;
using Olf.GoldenHorse.Foundation.Factories.ViewModels;

namespace Olf.GoldenHorse.Core.Factories.ViewModels
{
    public class TrainingItemViewModelFactory : ITrainingItemViewModelFactory
    {
        private Func<TestItem, ITrainingItemViewModel> createModelFunc;

        public TrainingItemViewModelFactory(Func<TestItem, ITrainingItemViewModel> createModelFunc)
        {
            this.createModelFunc = createModelFunc;
        }

        public ITrainingItemViewModel Create(TestItem testItem)
        {
            return createModelFunc(testItem);
        }
        
    }
}



