using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace Olf.GoldenHorse.Foundation.ViewModels
{
    public interface ITestDetailsViewModel : INotifyPropertyChanged
    {
        ITestItemViewModel SelectedTestItem { get; set; }
        ObservableCollection<ITestItemViewModel> TestItems { get; }
        ICommand PlayCommand { get; }
    }
}