

using System.Collections.ObjectModel;

namespace Olf.GoldenHorse.Foundation.ViewModels
{
    public interface ILogDetailsViewModel
    {
        ObservableCollection<ILogItemViewModel> LogItems { get; set; }
        ILogItemViewModel SelectedLogItem { get; set; }
    }
}