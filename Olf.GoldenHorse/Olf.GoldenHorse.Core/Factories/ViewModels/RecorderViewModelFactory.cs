using System;
using Olf.GoldenHorse.Foundation.Services;
using Olf.GoldenHorse.Foundation.ViewModels;
using Olf.GoldenHorse.Foundation.Factories.ViewModels;

namespace Olf.GoldenHorse.Core.Factories.ViewModels
{
    public class RecorderViewModelFactory : IRecorderViewModelFactory
    {
        private Func<IRecorder, IRecorderViewModel> createModelFunc;

        public RecorderViewModelFactory(Func<IRecorder, IRecorderViewModel> createModelFunc)
        {
            this.createModelFunc = createModelFunc;
        }

        public IRecorderViewModel Create(IRecorder recorder)
        {
            return createModelFunc(recorder);
        }
        
    }
}



