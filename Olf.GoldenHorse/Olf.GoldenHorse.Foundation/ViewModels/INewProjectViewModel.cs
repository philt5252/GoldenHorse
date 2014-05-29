

using System.Windows.Input;

namespace Olf.GoldenHorse.Foundation.ViewModels
{
    public interface INewProjectViewModel
    {
        string Name { get; set; }
        string Location { get; set; }

        ICommand BrowseCommand { get; }
        ICommand SaveNewProjectCommand { get; }
        ICommand CancelNewProjectCommand { get; }
    }
}