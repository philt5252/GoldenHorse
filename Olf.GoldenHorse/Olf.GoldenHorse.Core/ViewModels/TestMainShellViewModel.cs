using System.ComponentModel;
using System.Linq;
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
        private readonly IVariableManagerViewModelFactory variableManagerViewModelFactory;

        public ITestScreenshotsViewModel TestScreenshotsViewModel { get; protected set; }
        public ITestShellViewModel TestShellViewModel { get; protected set; }
        public IVariableManagerViewModel VariableManagerViewModel { get; protected set; }

        public Test Test
        {
            get { return test; }
        }

        public string TestName
        {
            get { return test.Name; }
        }

        public TestMainShellViewModel(Test test, ITestScreenshotsViewModelFactory testScreenshotsViewModelFactory,
            ITestShellViewModelFactory testShellViewModelFactory,
            IVariableManagerViewModelFactory variableManagerViewModelFactory)
        {
            this.test = test;
            this.testScreenshotsViewModelFactory = testScreenshotsViewModelFactory;
            this.testShellViewModelFactory = testShellViewModelFactory;
            this.variableManagerViewModelFactory = variableManagerViewModelFactory;

            TestScreenshotsViewModel = testScreenshotsViewModelFactory.Create(test);
            TestShellViewModel = testShellViewModelFactory.Create(test);
            VariableManagerViewModel = variableManagerViewModelFactory.Create(test);

            TestScreenshotsViewModel.PropertyChanged += TestScreenshotsViewModelOnPropertyChanged;
            TestShellViewModel.TestDetailsViewModel.PropertyChanged+=TestDetailsViewModelOnPropertyChanged;
        }

        private void TestDetailsViewModelOnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "SelectedTestItem")
            {
                if (TestShellViewModel.TestDetailsViewModel.SelectedTestItem == null
                    || !TestShellViewModel.TestDetailsViewModel.SelectedTestItem.HasScreenshot)
                    return;

                TestScreenshotsViewModel.SelectedScreenshot =
                    TestScreenshotsViewModel.Screenshots.FirstOrDefault(
                        s => s.Owner == TestShellViewModel.TestDetailsViewModel.SelectedTestItem.TestItem);
            }
        }

        private void TestScreenshotsViewModelOnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "SelectedScreenshot")
            {
                TestShellViewModel.TestDetailsViewModel.SelectedTestItem =
                    TestShellViewModel.TestDetailsViewModel.TestItems.FirstOrDefault(
                        t => t.TestItem == TestScreenshotsViewModel.SelectedScreenshot.Owner);

            }
        }
    }
}