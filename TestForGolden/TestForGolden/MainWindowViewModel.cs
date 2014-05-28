using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Documents;
using TestForGolden.Annotations;

namespace TestForGolden
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private IList<ITestItemViewModel> testItems;
        private Bitmap shownImage;
        private ITestItemViewModel selectedTestItemViewModel;

        public IList<ITestItemViewModel> TestItems
        {
            get { return testItems; }
            protected set { testItems = value; }
        }

        private void SetupScreenActions()
        {
            XmlFileWriter fileWriter = new XmlFileWriter();
            AppProcess process = new AppProcess{Name = "Process1"};
            AppWindow window = new AppWindow{Name="Window1"};
            AppWindow window2 = new AppWindow{Name="Window2"};
            AppControl control = new AppControl();
            AppControl control2 = new AppControl();

            process.Children.Add(window);
            process.Children.Add(window2);
            window.Children.Add(control);
            window2.Children.Add(control2);

            control.WindowId = window.Id;
            control2.WindowId = window2.Id;

            window.ProcessId = process.Id;
            window2.ProcessId = process.Id;

            OnScreenAction action1 = new OnScreenAction();
            action1.Operation = new ClickOperation();
            action1.Operation.Parameters[0].Value.DisplayValue = "1";
            action1.Operation.Parameters[1].Value.DisplayValue = "2";
            action1.ControlId = control.Id;
            action1.WindowId = window.Id;

            Screenshot screenShot = new Screenshot();
            screenShot.ImagePath = @"C:\Users\Phil\Desktop\screenshot.png";
            screenShot.Adornments.Add(new ScreenshotClickAdornment { ClickX = 100, ClickY = 100 });

            action1.Screenshot = screenShot;

            OnScreenAction action2 = new OnScreenAction();
            action2.Operation = new ClickOperation();
            action2.ControlId = control.Id;
            action2.WindowId = window.Id;

            OnScreenAction action3 = new OnScreenAction();
            action3.Operation = new ClickOperation();
            action3.ControlId = control.Id;
            action3.WindowId = window.Id;

            OnScreenAction action4 = new OnScreenAction();
            action4.Operation = new ClickOperation();
            action4.ControlId = control2.Id;
            action4.WindowId = window2.Id;

            OnScreenAction action5 = new OnScreenAction();
            action5.Operation = new ClickOperation();
            action5.ControlId = control2.Id;
            action5.WindowId = window2.Id;

            Screenshot screenShot2 = new Screenshot();
            screenShot2.ImagePath = @"C:\Users\Phil\Desktop\screenshot.png";
            screenShot2.Adornments.Add(new ScreenshotClickAdornment { ClickX = 300, ClickY = 250 });

            action5.Screenshot = screenShot2;

            OnScreenAction action6 = new OnScreenAction();
            action6.Operation = new ClickOperation();
            action6.ControlId = control2.Id;
            action6.WindowId = window2.Id;

            Test test = new Test();

            test.TestItems.Add(action1);
            test.TestItems.Add(action2);
            test.TestItems.Add(action3);
            test.TestItems.Add(action4);
            test.TestItems.Add(action5);
            test.TestItems.Add(action6);

            fileWriter.Write(test);

            AppManager appManager = new AppManager();
            appManager.Processes.Add(process);

            fileWriter.Write(appManager);
        }

        public ITestItemViewModel SelectedTestItemViewModel
        {
            get { return selectedTestItemViewModel; }
            set
            {
                selectedTestItemViewModel = value;
                if(selectedTestItemViewModel.HasScreenshot)
                    ShownImage = selectedTestItemViewModel.Screenshot.RenderImage();
                else
                {
                    ShownImage = null;
                }
            }
        }

        public MainWindowViewModel()
        {
            SetupScreenActions();
            XmlFileWriter fileWriter = new XmlFileWriter();

            Test test = fileWriter.Read();
            AppManager appManager = fileWriter.ReadAppManager();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            IList<ITestItemViewModel> testItemViewModels = new List<ITestItemViewModel>();

            ProcessGroupViewModel processTestItemViewModel = null;
            WindowGroupViewModel windowTestItemViewModel = null;

            foreach (TestItem testItem in test.TestItems)
            {
                if (testItem is OnScreenAction)
                {
                    OnScreenAction onScreenAction = testItem as OnScreenAction;

                    OnScreenActionViewModel onScreenActionViewModel =
                        new OnScreenActionViewModel(onScreenAction);

                    AppWindow appWindow = appManager.GetMappedItem<AppWindow>(onScreenAction.WindowId);

                    if (windowTestItemViewModel == null
                        || windowTestItemViewModel.WindowId != appWindow.Id)
                    {
                        windowTestItemViewModel = new WindowGroupViewModel();
                        windowTestItemViewModel.WindowId = appWindow.Id;
                        windowTestItemViewModel.Name = appWindow.Name;

                        AppProcess appProcess = appManager.GetMappedItem<AppProcess>(appWindow.ProcessId);

                        if (processTestItemViewModel == null
                            || processTestItemViewModel.ProcessId != appProcess.Id)
                        {
                            processTestItemViewModel = new ProcessGroupViewModel();
                            processTestItemViewModel.Name = "Process1";
                            processTestItemViewModel.ProcessId = appProcess.Id;
                            
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


           
        }

        public Bitmap ShownImage
        {
            get { return shownImage; }
            set
            {
                shownImage = value;
                OnPropertyChanged("ShownImage");
            }
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

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}