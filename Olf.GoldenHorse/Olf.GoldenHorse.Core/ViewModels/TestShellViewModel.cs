
using Olf.GoldenHorse.Foundation.Factories.ViewModels;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.ViewModels;

namespace Olf.GoldenHorse.Core.ViewModels
{
    public class TestShellViewModel : ITestShellViewModel
    {
        private readonly ITestDetailsViewModelFactory testDetailsViewModelFactory;
        private readonly ITestOperationsViewModelFactory testOperationsViewModelFactory;
        public ITestOperationsViewModel TestOperationsViewModel { get; protected set; }

        public ITestDetailsViewModel TestDetailsViewModel { get; protected set; }

        public TestShellViewModel(Test test, ITestDetailsViewModelFactory testDetailsViewModelFactory,
            ITestOperationsViewModelFactory testOperationsViewModelFactory)
        {
            this.testDetailsViewModelFactory = testDetailsViewModelFactory;
            this.testOperationsViewModelFactory = testOperationsViewModelFactory;

            TestDetailsViewModel = testDetailsViewModelFactory.Create(test);
            TestOperationsViewModel = testOperationsViewModelFactory.Create();
        }
    }
}