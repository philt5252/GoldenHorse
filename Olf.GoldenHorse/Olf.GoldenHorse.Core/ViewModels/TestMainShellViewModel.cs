
using Olf.GoldenHorse.Foundation.Factories.ViewModels;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.ViewModels;

namespace Olf.GoldenHorse.Core.ViewModels
{
    public class TestMainShellViewModel : ITestMainShellViewModel
    {
        private readonly Test test;
        private readonly ITestScreenshotsViewModelFactory testScreenshotsViewModelFactory;
        private readonly ITestShellViewModelFactory testShellViewModelFactory;

        public ITestScreenshotsViewModel TestScreenshotsViewModel { get; protected set; }
        public ITestShellViewModel TestShellViewModel { get; protected set; }

        public string TestName
        {
            get { return test.Name; }
        }

        public TestMainShellViewModel(Test test, ITestScreenshotsViewModelFactory testScreenshotsViewModelFactory,
            ITestShellViewModelFactory testShellViewModelFactory)
        {
            this.test = test;
            this.testScreenshotsViewModelFactory = testScreenshotsViewModelFactory;
            this.testShellViewModelFactory = testShellViewModelFactory;

            TestScreenshotsViewModel = testScreenshotsViewModelFactory.Create();
            TestShellViewModel = testShellViewModelFactory.Create();
        }

        
    }
}