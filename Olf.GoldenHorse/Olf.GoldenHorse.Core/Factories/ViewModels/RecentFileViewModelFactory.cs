
using System;
using Olf.GoldenHorse.Foundation.ViewModels;
using Olf.GoldenHorse.Foundation.Factories.ViewModels;

namespace Olf.GoldenHorse.Core.Factories.ViewModels
{
    public class RecentFileViewModelFactory : IRecentFileViewModelFactory
    {
        private Func<string, IRecentFileViewModel> createModelFunc;

        public RecentFileViewModelFactory(Func<string, IRecentFileViewModel> createModelFunc)
        {
            this.createModelFunc = createModelFunc;
        }

        public IRecentFileViewModel Create(string filePath)
        {
            return createModelFunc(filePath);
        }
        
    }
}



