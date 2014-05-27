using System;
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
            XmlFileWriter fileWriter = new XmlFileWriter();

            Test test = fileWriter.Read();
            AppManager appManager = fileWriter.ReadAppManager();

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

            
        } 
    }
}