using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Olf.GoldenHorse.Foundation.DataAccess;
using Olf.GoldenHorse.Foundation.Factories.ViewModels;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.ViewModels;

namespace Olf.GoldenHorse.Core.ViewModels
{
    public class TestMainShellViewModel : ViewModelBase, ITestMainShellViewModel
    {
        private readonly Test test;
        private string testName;
        private readonly ITestScreenshotsViewModelFactory testScreenshotsViewModelFactory;
        private readonly ITestShellViewModelFactory testShellViewModelFactory;
        private readonly IVariableManagerViewModelFactory variableManagerViewModelFactory;
        private readonly ITestFileManager testFileManager;

        public ITestScreenshotsViewModel TestScreenshotsViewModel { get; protected set; }
        public ITestShellViewModel TestShellViewModel { get; protected set; }
        public IVariableManagerViewModel VariableManagerViewModel { get; protected set; }

        public ICommand SaveCommand { get; protected set; }

        public Test Test
        {
            get { return test; }
        }

        public string TestName
        {
            get { return testName; }
            set
            {
                testName = value;
                OnPropertyChanged("TestName");
            }
        }

        public TestMainShellViewModel(Test test, ITestScreenshotsViewModelFactory testScreenshotsViewModelFactory,
            ITestShellViewModelFactory testShellViewModelFactory,
            IVariableManagerViewModelFactory variableManagerViewModelFactory,
            ITestFileManager testFileManager)
        {
            this.test = test;
            this.testScreenshotsViewModelFactory = testScreenshotsViewModelFactory;
            this.testShellViewModelFactory = testShellViewModelFactory;
            this.variableManagerViewModelFactory = variableManagerViewModelFactory;
            this.testFileManager = testFileManager;
            testName = test.Name;

            SaveCommand = new DelegateCommand(ExecuteSaveCommand);

            TestScreenshotsViewModel = testScreenshotsViewModelFactory.Create(test);
            TestShellViewModel = testShellViewModelFactory.Create(test);
            VariableManagerViewModel = variableManagerViewModelFactory.Create(test);

            TestScreenshotsViewModel.PropertyChanged += TestScreenshotsViewModelOnPropertyChanged;
            TestShellViewModel.TestDetailsViewModel.PropertyChanged+=TestDetailsViewModelOnPropertyChanged;
            Test.TestChanged += TestOnTestChanged;
        }

        private void TestOnTestChanged(object sender, EventArgs eventArgs)
        {
            if (!TestName.EndsWith("*"))
                TestName += "*";
        }

        private void ExecuteSaveCommand()
        {
            testFileManager.Save(test);
            TestName = Test.Name;
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
                //TestShellViewModel.TestDetailsViewModel.SelectedTestItem =
                //    TestShellViewModel.TestDetailsViewModel.TestItems.FirstOrDefault(
                //        t => t.TestItem != null && t.TestItem.Id == TestScreenshotsViewModel.SelectedScreenshot.Owner.Id);

            }
        }
    }
}