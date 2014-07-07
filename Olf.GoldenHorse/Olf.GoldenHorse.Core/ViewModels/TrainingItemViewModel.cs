
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.ViewModels;

namespace Olf.GoldenHorse.Core.ViewModels
{
    public class TrainingItemViewModel : ViewModelBase, ITrainingItemViewModel
    {
        private readonly TestItem testItem;
        private TrainingItemStatus status;

        public TrainingItemStatus Status
        {
            get { return status; }
            set
            {
                status = value; 
                OnPropertyChanged("Status");
            }
        }

        public TestItem TestItem { get { return testItem; } }

        public string Description { get { return testItem.Description; } }

        public TrainingItemViewModel(TestItem testItem)
        {
            this.testItem = testItem;
        }
    }
}