
using Olf.GoldenHorse.Foundation.Factories.ViewModels;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.ViewModels;

namespace Olf.GoldenHorse.Core.ViewModels
{
    public class TestShellViewModel : ITestShellViewModel
    {
        private readonly ITestDetailsViewModelFactory testDetailsViewModelFactory;
        public ITestOperationsViewModel TestOperationsViewModel { get; protected set; }

        public ITestDetailsViewModel TestDetailsViewModel { get; protected set; }

        public TestShellViewModel(Test test, ITestDetailsViewModelFactory testDetailsViewModelFactory)
        {
            this.testDetailsViewModelFactory = testDetailsViewModelFactory;

           TestDetailsViewModel = testDetailsViewModelFactory.Create(test);
        }
    }
}