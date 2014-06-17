
using System;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.ViewModels;
using Olf.GoldenHorse.Foundation.Factories.ViewModels;

namespace Olf.GoldenHorse.Core.Factories.ViewModels
{
    public class VariableTableEditViewModelFactory : IVariableTableEditViewModelFactory
    {
        private Func<Variable, IVariableTableEditViewModel> createModelFunc;

        public VariableTableEditViewModelFactory(Func<Variable, IVariableTableEditViewModel> createModelFunc)
        {
            this.createModelFunc = createModelFunc;
        }

        public IVariableTableEditViewModel Create(Variable variable)
        {
            return createModelFunc(variable);
        }
        
    }
}



