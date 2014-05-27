using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Documents;

namespace TestForGolden
{
    public class MainWindowViewModel
    {
        private IList<ITestItemViewModel> testItems;

        public IList<ITestItemViewModel> TestItems
        {
            get { return testItems; }
            protected set { testItems = value; }
        }

        public MainWindowViewModel()
        {
            XmlFileWriter fileWriter = new XmlFileWriter();

            Test test = fileWriter.Read();
            AppManager appManager = fileWriter.ReadAppManager();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            IList<ITestItemViewModel> testItemViewModels = new List<ITestItemViewModel>();

            ITestItemViewModel processTestItemViewModel = null;
            ITestItemViewModel windowTestItemViewModel = null;

            foreach (TestItem testItem in test.TestItems)
            {
                if (testItem is OnScreenAction)
                {
                    OnScreenAction onScreenAction = testItem as OnScreenAction;

                    OnScreenActionViewModel onScreenActionViewModel =
                        new OnScreenActionViewModel(onScreenAction);

                    AppWindow appWindow = appManager.GetMappedItem<AppWindow>(onScreenAction.WindowId);

                    if (windowTestItemViewModel == null
                        || windowTestItemViewModel.Name != appWindow.Name)
                    {
                        windowTestItemViewModel = new WindowGroupViewModel();
                        windowTestItemViewModel.Name = appWindow.Name;

                        AppProcess appProcess = appManager.GetMappedItem<AppProcess>(appWindow.ProcessId);

                        if (processTestItemViewModel == null
                            || processTestItemViewModel.Name != appProcess.Name)
                        {
                            processTestItemViewModel = new ProcessGroupViewModel();
                            processTestItemViewModel.Name = "Process1";
                            
                            testItemViewModels.Add(processTestItemViewModel);
                        }

                        processTestItemViewModel.ChildItems.Add(windowTestItemViewModel);
                    }

                    windowTestItemViewModel.ChildItems.Add(onScreenActionViewModel);
                }
            }

            TestItems = testItemViewModels.ToArray();
            stopwatch.Stop();

            stopwatch.Reset();
            stopwatch.Start();
            List<TestItem> items = GetTestItems(testItems);
            stopwatch.Stop();

            ClickOperation operation = new ClickOperation();
            
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
                    testItemViewModel.TestItem.Children = new List<TestItem>();
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