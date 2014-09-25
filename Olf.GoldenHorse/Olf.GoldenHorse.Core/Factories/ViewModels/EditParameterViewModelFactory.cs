
using System;
using Olf.GoldenHorse.Core.ViewModels;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.ViewModels;
using Olf.GoldenHorse.Foundation.Factories.ViewModels;

namespace Olf.GoldenHorse.Core.Factories.ViewModels
{
    public class EditParameterViewModelFactory : IEditParameterViewModelFactory
    {
        private Func<OperationParameter, IEditParameterViewModel> createViewModelFunc;
        private readonly Func<OperationParameter, IEditImageParameterViewModel> createImageViewModelFunc;

        public EditParameterViewModelFactory(Func<OperationParameter, IEditParameterViewModel> createViewModelFunc,
            Func<OperationParameter, IEditImageParameterViewModel> createImageViewModelFunc  )
        {
            this.createViewModelFunc = createViewModelFunc;
            this.createImageViewModelFunc = createImageViewModelFunc;
        }

        public IEditParameterViewModel Create(OperationParameter parameter)
        {
            if(!parameter.TypeIdentifier.Equals("Image"))
                return createViewModelFunc(parameter);

            return createImageViewModelFunc(parameter);
        }
        
    }
}



