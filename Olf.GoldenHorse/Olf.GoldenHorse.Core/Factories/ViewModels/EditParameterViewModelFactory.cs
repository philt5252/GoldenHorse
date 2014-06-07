
using System;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.ViewModels;
using Olf.GoldenHorse.Foundation.Factories.ViewModels;

namespace Olf.GoldenHorse.Core.Factories.ViewModels
{
    public class EditParameterViewModelFactory : IEditParameterViewModelFactory
    {
        private Func<OperationParameter, IEditParameterViewModel> createModelFunc;

        public EditParameterViewModelFactory(Func<OperationParameter, IEditParameterViewModel> createModelFunc)
        {
            this.createModelFunc = createModelFunc;
        }

        public IEditParameterViewModel Create(OperationParameter parameter)
        {
            return createModelFunc(parameter);
        }
        
    }
}



