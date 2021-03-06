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
        ICommand DeleteSelectedItemCommand { get; }
        ICommand AppendToEndOfTestCommand { get; }
        ICommand AppendToStartOfTestCommand { get; }
        ICommand AppendAfterSelectedItemCommand { get; }
        void Refresh();
    }
}