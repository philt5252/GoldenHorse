using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            AppManager appManager = new AppManager();
            AppProcess process = new AppProcess();
            AppWindow window = new AppWindow();
            AppControl control = new AppControl();
            window.Id = "w1";
            window.Name = "Window1";
            control.Id = "c1";
            control.Name = "Control1";
            process.Id = "p1";
            process.Name = "Process1";
            window.ProcessId = process.Id;
            OnScreenAction mouseAction = new OnScreenAction();
            mouseAction.ControlId = control.Id;
            mouseAction.WindowId = window.Id;
            mouseAction.Description = "MOUSE DESCRIPTION!";

            OnScreenAction mouseAction1 = new OnScreenAction();
            mouseAction1.ControlId = control.Id;
            mouseAction1.WindowId = window.Id;
            mouseAction1.Description = "MOUSE DESCRIPTION1111!";

            OnScreenAction mouseAction2 = new OnScreenAction();
            mouseAction2.ControlId = control.Id;
            mouseAction2.WindowId = window.Id;
            mouseAction2.Description = "MOUSE DESCRIPTION22222!";

            appManager.SetAppControl(control);
            appManager.SetAppWindow(window);
            appManager.SetAppProcess(process);
            
            List<TestItem> testItems = new List<TestItem>();
            testItems.Add(mouseAction);
            testItems.Add(mouseAction1);
            testItems.Add(mouseAction2);

            IList<ITestItemViewModel> testItemViewModels = new List<ITestItemViewModel>();

            ITestItemViewModel processTestItemViewModel = null;
            ITestItemViewModel windowTestItemViewModel = null;

            foreach (TestItem testItem in testItems)
            {
                if (testItem is OnScreenAction)
                {
                    OnScreenAction onScreenAction = testItem as OnScreenAction;

                    OnScreenActionViewModel onScreenActionViewModel =
                        new OnScreenActionViewModel(onScreenAction);

                    IAppWindow appWindow = appManager.GetAppWindow(onScreenAction.WindowId);

                    if (windowTestItemViewModel == null
                        || windowTestItemViewModel.Name != appWindow.Name)
                    {
                        windowTestItemViewModel = new WindowGroupViewModel();
                        windowTestItemViewModel.Name = appWindow.Name;

                        IAppProcess appProcess = appManager.GetAppProcess(appWindow.ProcessId);

                        if (processTestItemViewModel == null
                            || processTestItemViewModel.Name != appProcess.Name)
                        {
                            processTestItemViewModel = new ProcessGroupViewModel();
                            processTestItemViewModel.Name = "Process1";
                            processTestItemViewModel.ChildItems.Add(windowTestItemViewModel);

                            testItemViewModels.Add(processTestItemViewModel);

                        }
                    }

                    windowTestItemViewModel.ChildItems.Add(onScreenActionViewModel);
                }
            }

            TestItems = testItemViewModels.ToArray();

            Test test = new Test();
            test.TestItems = testItems;

            XmlFileWriter fileWriter = new XmlFileWriter();
            fileWriter.Write(test);

            Test testRead = fileWriter.Read();
        } 
    }
}