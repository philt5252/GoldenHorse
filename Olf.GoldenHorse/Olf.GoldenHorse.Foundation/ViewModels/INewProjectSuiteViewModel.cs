

using System.Windows.Input;

namespace Olf.GoldenHorse.Foundation.ViewModels
{
    public interface INewProjectSuiteViewModel
    {
        string Name { get; set; }
        string Location { get; set; }

        ICommand BrowseCommand { get; }
        ICommand SaveNewProjectSuiteCommand { get; }
        ICommand CancelNewProjectSuiteCommand { get; }
    }
}