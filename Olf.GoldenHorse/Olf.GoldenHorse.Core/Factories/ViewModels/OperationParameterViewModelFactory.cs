using System;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.ViewModels;
using Olf.GoldenHorse.Foundation.Factories.ViewModels;

namespace Olf.GoldenHorse.Core.Factories.ViewModels
{
    public class OperationParameterViewModelFactory : IOperationParameterViewModelFactory
    {
        private Func<OperationParameter, IOperationParameterViewModel> createModelFunc;

        public OperationParameterViewModelFactory(Func<OperationParameter, IOperationParameterViewModel> createModelFunc)
        {
            this.createModelFunc = createModelFunc;
        }

        public IOperationParameterViewModel Create(OperationParameter parameter)
        {
            return createModelFunc(parameter);
        }
        
    }
}



