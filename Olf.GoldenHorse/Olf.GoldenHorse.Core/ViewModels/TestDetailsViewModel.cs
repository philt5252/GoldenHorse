using System;
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
            ILogFileManager logFileManager, ILogController logController)
        {
            this.test = test;
            this.testItemViewModelFactory = testItemViewModelFactory;
            this.logFileManager = logFileManager;
            this.logController = logController;
            TestItems = new ObservableCollection<ITestItemViewModel>(test.TestItems.Select(testItemViewModelFactory.Create));
        
            PlayCommand = new DelegateCommand(ExecutePlayCommand);
        }

        private void ExecutePlayCommand()
        {
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
        }
    }
}