using System;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.ViewModels;
using Olf.GoldenHorse.Foundation.Factories.ViewModels;

namespace Olf.GoldenHorse.Core.Factories.ViewModels
{
    public class VariableManagerViewModelFactory : IVariableManagerViewModelFactory
    {
        private Func<Test, IVariableManagerViewModel> createModelFunc;

        public VariableManagerViewModelFactory(Func<Test, IVariableManagerViewModel> createModelFunc)
        {
            this.createModelFunc = createModelFunc;
        }

        public IVariableManagerViewModel Create(Test  test)
        {
            return createModelFunc(test);
        }
        
    }
}



