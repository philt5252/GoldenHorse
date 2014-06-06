

using System.Windows.Input;

namespace Olf.GoldenHorse.Foundation.ViewModels
{
    public interface IStartPageViewModel
    {
        string Version { get; }
        IRecentFileViewModel[] RecentFiles { get; }
        ICommand NewProjectSuiteCommand { get; }
        ICommand OpenProjectSuiteCommand { get; }
    }
}