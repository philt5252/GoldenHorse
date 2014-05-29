

using System.Windows.Input;

namespace Olf.GoldenHorse.Foundation.ViewModels
{
    public interface INewProjectViewModel
    {
        string Name { get; set; }
        string Location { get; set; }

        ICommand BrowseCommand { get; }
        ICommand SaveCommand { get; }
        ICommand CancelCommand { get; }
    }
}