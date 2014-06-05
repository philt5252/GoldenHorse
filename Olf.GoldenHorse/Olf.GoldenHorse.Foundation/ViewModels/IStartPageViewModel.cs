

using System.Windows.Input;

namespace Olf.GoldenHorse.Foundation.ViewModels
{
    public interface IStartPageViewModel
    {
        string Version { get; }
        IRecentFileViewModel[] RecentFiles { get; }
        ICommand CreateNewProjectCommand { get; }
        ICommand OpenProjectCommand { get; }
    }
}