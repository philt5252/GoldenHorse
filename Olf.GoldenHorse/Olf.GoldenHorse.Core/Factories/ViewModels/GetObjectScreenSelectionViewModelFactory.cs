using System;
using Olf.GoldenHorse.Foundation.Factories.ViewModels;
using Olf.GoldenHorse.Foundation.ViewModels;

namespace Olf.GoldenHorse.Core.Factories.ViewModels
{
    public class GetObjectScreenSelectionViewModelFactory : IGetObjectScreenSelectionViewModelFactory
    {
        private readonly Func<IGetObjectScreenSelectionViewModel> createViewModelFunc;

        public GetObjectScreenSelectionViewModelFactory(Func<IGetObjectScreenSelectionViewModel> createViewModelFunc )
        {
            this.createViewModelFunc = createViewModelFunc;
        }

        public IGetObjectScreenSelectionViewModel Create()
        {
            return createViewModelFunc();
        }
    }
}