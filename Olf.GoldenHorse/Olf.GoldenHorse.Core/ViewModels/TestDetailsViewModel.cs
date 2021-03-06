using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Input;
using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Olf.GoldenHorse.Foundation.Controllers;
using Olf.GoldenHorse.Foundation.DataAccess;
using Olf.GoldenHorse.Foundation.Events;
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
        private readonly IRecordingController recordingController;
        private readonly ITrainingController trainingController;
        private ITestItemViewModel selectedTestItem;
        private ObservableCollection<ITestItemViewModel> testItems;

        public ObservableCollection<ITestItemViewModel> TestItems
        {
            get { return testItems; }
            protected set
            {
                if (testItems != null)
                {
                    testItems.CollectionChanged -= TestItemsOnCollectionChanged;

                    UnsubscribeTestItems(testItems);
                }
                
                testItems = value; 

                testItems.CollectionChanged += TestItemsOnCollectionChanged;

                SubscribeTestItems(testItems);

                foreach (ITestItemViewModel testItemViewModel in testItems)
                {
                    testItemViewModel.ChildItems.CollectionChanged += TestItemsOnCollectionChanged;
                }
            }
        }

        private void SubscribeTestItems(IList<ITestItemViewModel> testItemViewModels )
        {
            foreach (var testItemViewModel in testItemViewModels)
            {
                testItemViewModel.ChildItems.CollectionChanged += TestItemsOnCollectionChanged;
                SubscribeTestItems(testItemViewModel.ChildItems);
            }
        }

        private void UnsubscribeTestItems(IList<ITestItemViewModel> testItemViewModels)
        {
            foreach (var testItemViewModel in testItemViewModels)
            {
                testItemViewModel.ChildItems.CollectionChanged -= TestItemsOnCollectionChanged;
                UnsubscribeTestItems(testItemViewModel.ChildItems);
            }
        }

        private void TestItemsOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            foreach (ITestItemViewModel testItemViewModel in sender as IList<ITestItemViewModel>)
            {
                testItemViewModel.ChildItems.CollectionChanged -= TestItemsOnCollectionChanged;
                testItemViewModel.ChildItems.CollectionChanged += TestItemsOnCollectionChanged;
            }

            test.TestItems = new ObservableCollection<TestItem>(GetTestItems(TestItems));
            //RefreshTestItems();
        }

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
        public ICommand DeleteSelectedItemCommand { get; private set; }
        public ICommand RunAsTrainingCommand { get; protected set; }

        public ICommand AppendToEndOfTestCommand { get; protected set; }
        public ICommand AppendToStartOfTestCommand { get; protected set; }
        public ICommand AppendAfterSelectedItemCommand { get; protected set; }
        public ICommand RefreshCommand { get; protected set; }
 
        public TestDetailsViewModel(Test test, ITestItemViewModelFactory testItemViewModelFactory,
            ILogFileManager logFileManager, ILogController logController, 
            IAppController appController, IRecordingController recordingController,
            ITrainingController trainingController,
            IEventAggregator eventAggregator)
        {
            this.test = test;
            this.testItemViewModelFactory = testItemViewModelFactory;
            this.logFileManager = logFileManager;
            this.logController = logController;
            this.appController = appController;
            this.recordingController = recordingController;
            this.trainingController = trainingController;

            eventAggregator.GetEvent<AddTestItemEvent>().Subscribe(AddTestItemEventHandler);
            //TestItems = new ObservableCollection<ITestItemViewModel>(test.TestItems.Select(testItemViewModelFactory.Create));
            RefreshTestItems();
            PlayCommand = new DelegateCommand(ExecutePlayCommand);
            AppendToEndOfTestCommand = new DelegateCommand(ExecuteAppendToEndOfTestCommand);
            AppendToStartOfTestCommand = new DelegateCommand(ExecuteAppendToStartOfTestCommand);
            AppendAfterSelectedItemCommand = new DelegateCommand(ExecuteAppendAfterSelectedItemCommand);
            DeleteSelectedItemCommand = new DelegateCommand(ExecuteDeleteSelectedItemCommand);
            RefreshCommand = new DelegateCommand(ExecuteRefresh);
            RunAsTrainingCommand = new DelegateCommand(EecuteRunAsTrainingCommand);
        }

        private void EecuteRunAsTrainingCommand()
        {
            trainingController.Start(test);
        }

        private void ExecuteRefresh()
        {
            RefreshTestItems();
        }

        private void ExecuteAppendAfterSelectedItemCommand()
        {
            if (SelectedTestItem != null && SelectedTestItem.TestItem != null)
            {
                int index = test.TestItems.IndexOf(SelectedTestItem.TestItem);

                recordingController.AppendAtIndex(test, index+1);
            }          
        }

        private void ExecuteAppendToStartOfTestCommand()
        {
            recordingController.AppendToStart(test);
        }

        private void AddTestItemEventHandler(TestItem testItem)
        {
            
            if (SelectedTestItem != null && SelectedTestItem.TestItem != null)
            {
                int index = test.TestItems.IndexOf(SelectedTestItem.TestItem);
                test.TestItems.Insert(index, testItem);
            }
            else
            {
                test.TestItems.Add(testItem);
            }

            RefreshTestItems();

            SelectedTestItem = TestItems.FirstOrDefault(t => t.TestItem == testItem);

            /*
            ITestItemViewModel testitemViewModel = testItemViewModelFactory.Create(testItem);
            testItem.Test = test;

            if (SelectedTestItem == null)
            {
                TestItems.Add(testitemViewModel);
                return;
            }

            int index = TestItems.IndexOf(SelectedTestItem);

            if (index >=0)
            {
                TestItems.Insert(index, testitemViewModel);
                return;
            }

            ITestItemViewModel testItemViewModel = GetParent(SelectedTestItem);

            index = testItemViewModel.ChildItems.IndexOf(SelectedTestItem);

            testItemViewModel.ChildItems.Insert(index, testitemViewModel);*/
        }

        private ITestItemViewModel GetParent(ITestItemViewModel testItemViewModel, ITestItemViewModel[] testItems=null)
        {
            if (testItems == null)
                testItems = TestItems.ToArray();

            foreach (ITestItemViewModel potentialParent in testItems)
            {
                if (potentialParent.ChildItems.Contains(testItemViewModel))
                    return potentialParent;

                ITestItemViewModel result = GetParent(testItemViewModel, potentialParent.ChildItems.ToArray());

                if (result != null)
                    return result;
            }

            return null;
        }

        private void ExecuteDeleteSelectedItemCommand()
        {
            if (SelectedTestItem == null)
                return;

            //int index = TestItems.IndexOf(SelectedTestItem);
            //TestItems.RemoveAt(index);

            test.TestItems.Remove(SelectedTestItem.TestItem);

            RefreshTestItems();

            //if (TestItems.Count <= index)
            //    index = TestItems.Count - 1;

            //SelectedTestItem = TestItems[index];
        }

        private void ExecuteAppendToEndOfTestCommand()
        {
            recordingController.AppendToTest(test);
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


            if(SelectedTestItem == null || SelectedTestItem.TestItem == null)
                test.Play(log);
            else
                test.Play(log, SelectedTestItem.TestItem.Id);

            log.EndTime = DateTime.Now;

            logFileManager.Save(log);

            logController.ShowLog(log);

            appController.MainWindow.Restore();
        }

        private void RefreshTestItems()
        {
            IList<ITestItemViewModel> testItemViewModels = new List<ITestItemViewModel>();

            CreateTestDetailsViewModels(test.TestItems, ref testItemViewModels);

            TestItems = new ObservableCollection<ITestItemViewModel>(testItemViewModels); ;

            OnPropertyChanged("TestItems");
        }

        private void CreateTestDetailsViewModels(IList<TestItem> testItems, ref IList<ITestItemViewModel> finalList )
        {
            ITestItemViewModel processTestItemViewModel = null;
            ITestItemViewModel windowTestItemViewModel = null;
            IList<ITestItemViewModel> testItemViewModels = new List<ITestItemViewModel>();

            foreach (TestItem testItem in testItems)
            {
                ITestItemViewModel testItemViewModel = testItemViewModelFactory.Create(testItem);

                if (testItem.Type == TestItemTypes.OnScreenAction
                    || testItem.Type == TestItemTypes.ValidateTextAtPoint)
                {
                    if (testItem.Control == null)
                    {
                        testItemViewModels.Add(testItemViewModel);
                        windowTestItemViewModel = null;
                        processTestItemViewModel = null;
                        continue;
                    }

                    MappedItem window = test.Project.AppManager.GetWindow(testItem.Control);
                    if (window == null)
                    {

                    }

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
                else
                {
                    testItemViewModels.Add(testItemViewModel);
                    windowTestItemViewModel = null;
                    processTestItemViewModel = null;
                }

                if (testItem.TestItems != null)
                {
                    IList<ITestItemViewModel> childrenList = new List<ITestItemViewModel>();
                    CreateTestDetailsViewModels(testItem.TestItems, ref childrenList);

                    testItemViewModel.ChildItems.AddRange(childrenList);
                }
            }

            finalList = testItemViewModels;
        }

        private List<TestItem> GetTestItems(IEnumerable<ITestItemViewModel> testItemViewModels)
        {
            List<TestItem> testItems = new List<TestItem>();

            GetTestItems(testItemViewModels, testItems, null);

            return testItems;
        }

        private void GetTestItems(IEnumerable<ITestItemViewModel> testItemViewModels, List<TestItem> testItems, TestItem parentTestItem)
        {
            TestItem newParentTestItem = parentTestItem;
            foreach (ITestItemViewModel testItemViewModel in testItemViewModels)
            {
                if (testItemViewModel.TestItem != null )
                {
                    testItemViewModel.TestItem.TestItems = new ObservableCollection<TestItem>();
                    newParentTestItem = testItemViewModel.TestItem;

                    if (parentTestItem != null)
                        parentTestItem.TestItems.Add(testItemViewModel.TestItem);
                    else
                    {
                        
                        testItems.Add(newParentTestItem);
                    }
                }
                else
                {
                    //newParentTestItem = null;
                }
                GetTestItems(testItemViewModel.ChildItems, testItems, newParentTestItem);
            }
        }

        public void Refresh()
        {
            RefreshTestItems();
        }

    }
}