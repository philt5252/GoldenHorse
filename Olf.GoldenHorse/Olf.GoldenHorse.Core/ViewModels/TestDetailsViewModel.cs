using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Olf.GoldenHorse.Foundation.Controllers;
using Olf.GoldenHorse.Foundation.DataAccess;
using Olf.GoldenHorse.Foundation.Factories.ViewModels;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.ViewModels;

namespace Olf.GoldenHorse.Core.ViewModels
{
    public class TestDetailsViewModel : ViewModelBase, ITestDetailsViewModel
    {
        private readonly Test test;
        private readonly ITestItemViewModelFactory testItemViewModelFactory;
        private readonly ILogFileManager logFileManager;
        private readonly ILogController logController;
        private readonly IAppController appController;
        private ITestItemViewModel selectedTestItem;
        public ObservableCollection<ITestItemViewModel> TestItems { get; protected set; }

        public ITestItemViewModel SelectedTestItem
        {
            get { return selectedTestItem; }
            set
            {
                if (Equals(selectedTestItem, value))
                    return;

                selectedTestItem = value;
                OnPropertyChanged("SelectedTestItem");
            }
        }

        public ICommand PlayCommand { get; protected set; }
 
        public TestDetailsViewModel(Test test, ITestItemViewModelFactory testItemViewModelFactory,
            ILogFileManager logFileManager, ILogController logController, IAppController appController)
        {
            this.test = test;
            this.testItemViewModelFactory = testItemViewModelFactory;
            this.logFileManager = logFileManager;
            this.logController = logController;
            this.appController = appController;
            //TestItems = new ObservableCollection<ITestItemViewModel>(test.TestItems.Select(testItemViewModelFactory.Create));
            RefreshTestItems();
            PlayCommand = new DelegateCommand(ExecutePlayCommand);
        }

        private void ExecutePlayCommand()
        {
            appController.MainWindow.Minimize();

            Log log = new Log();
            log.Owner = test.Project;
            DateTime dateTime = DateTime.Now;
            log.StartTime = dateTime;
            log.Name = string.Format("{0}_{1}_{2}_{3}_{4}_{5}",
                dateTime.Month, dateTime.Day, dateTime.Year,
                dateTime.Hour, dateTime.Minute, dateTime.Second);

            foreach (TestItem testItem in test.TestItems)
            {
                testItem.Play(log);
            }

            log.EndTime = DateTime.Now;

            logFileManager.Save(log);

            logController.ShowLog(log);

            appController.MainWindow.Restore();
        }

        private void RefreshTestItems()
        {
            ITestItemViewModel processTestItemViewModel = null;
            ITestItemViewModel windowTestItemViewModel = null;
            IList<ITestItemViewModel> testItemViewModels = new List<ITestItemViewModel>();

            foreach (TestItem testItem in test.TestItems)
            {
                if (testItem.Type == TestItemTypes.OnScreenAction)
                {
                    ITestItemViewModel testItemViewModel = testItemViewModelFactory.Create(testItem);

                    MappedItem window = test.Project.AppManager.GetWindow(testItem.Control);

                    if (windowTestItemViewModel == null
                        || windowTestItemViewModel.ControlId != window.Id)
                    {
                        windowTestItemViewModel = testItemViewModelFactory.Create(null);
                        windowTestItemViewModel.ControlId = window.Id;
                        windowTestItemViewModel.Type = TestItemTypes.WindowGroup;
                        windowTestItemViewModel.Name = window.FriendlyName;

                        AppProcess appProcess = test.Project.AppManager.GetProcess(testItem.Control);

                        if (processTestItemViewModel == null
                            || processTestItemViewModel.ControlId != appProcess.Id)
                        {
                            processTestItemViewModel = testItemViewModelFactory.Create(null);
                            processTestItemViewModel.Name = appProcess.Name;
                            processTestItemViewModel.Type = TestItemTypes.ProcessGroup;
                            processTestItemViewModel.ControlId = appProcess.Id;

                            testItemViewModels.Add(processTestItemViewModel);
                        }

                        processTestItemViewModel.ChildItems.Add(windowTestItemViewModel);
                    }

                    windowTestItemViewModel.ChildItems.Add(testItemViewModel);
                }
            }

            TestItems = new ObservableCollection<ITestItemViewModel>(testItemViewModels);
        }

        private List<TestItem> GetTestItems(IEnumerable<ITestItemViewModel> testItemViewModels)
        {
            List<TestItem> testItems = new List<TestItem>();

            GetTestItems(testItemViewModels, testItems, null);

            return testItems;
        }

        private void GetTestItems(IEnumerable<ITestItemViewModel> testItemViewModels, List<TestItem> testItems, TestItem parentTestItem)
        {
            TestItem newParentTestItem = null;
            foreach (ITestItemViewModel testItemViewModel in testItemViewModels)
            {
                if (testItemViewModel.TestItem != null)
                {
                    testItemViewModel.TestItem.Children = new ObservableCollection<TestItem>();
                    if (parentTestItem != null)
                        parentTestItem.Children.Add(testItemViewModel.TestItem);
                    else
                    {
                        newParentTestItem = testItemViewModel.TestItem;
                        testItems.Add(newParentTestItem);
                    }
                }
                else
                {
                    newParentTestItem = null;
                }
                GetTestItems(testItemViewModel.ChildItems, testItems, newParentTestItem);
            }
        }

    }
}