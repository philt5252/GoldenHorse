
using System;
using Olf.GoldenHorse.Foundation.ViewModels;
using Olf.GoldenHorse.Foundation.Factories.ViewModels;

namespace Olf.GoldenHorse.Core.Factories.ViewModels
{
    public class EditParameterViewModelFactory : IEditParameterViewModelFactory
    {
        private Func<IEditParameterViewModel> createModelFunc;

        public EditParameterViewModelFactory(Func<IEditParameterViewModel> createModelFunc)
        {
            this.createModelFunc = createModelFunc;
        }

        public IEditParameterViewModel Create()
        {
            return createModelFunc();
        }
        
    }
}



