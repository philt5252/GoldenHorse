using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Olf.GoldenHorse.Foundation.ViewModels
{
    public interface ILogDetailsViewModel : INotifyPropertyChanged
    {
        ObservableCollection<ILogItemViewModel> LogItems { get; set; }
        ILogItemViewModel SelectedLogItem { get; set; }
    }
}