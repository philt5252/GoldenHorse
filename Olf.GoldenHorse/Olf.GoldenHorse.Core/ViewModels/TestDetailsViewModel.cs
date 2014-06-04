using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Olf.GoldenHorse.Foundation.Factories.ViewModels;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.ViewModels;

namespace Olf.GoldenHorse.Core.ViewModels
{
    public class TestDetailsViewModel : ViewModelBase, ITestDetailsViewModel
    {
        private readonly Test test;
        private readonly ITestItemViewModelFactory testItemViewModelFactory;
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
 
        public TestDetailsViewModel(Test test, ITestItemViewModelFactory testItemViewModelFactory)
        {
            this.test = test;
            this.testItemViewModelFactory = testItemViewModelFactory;
            TestItems = new ObservableCollection<ITestItemViewModel>(test.TestItems.Select(testItemViewModelFactory.Create));
        
            PlayCommand = new DelegateCommand(ExecutePlayCommand);
        }

        private void ExecutePlayCommand()
        {
            Log log = new Log();

            foreach (TestItem testItem in test.TestItems)
            {
                testItem.Play(log);
            }
        }
    }
}