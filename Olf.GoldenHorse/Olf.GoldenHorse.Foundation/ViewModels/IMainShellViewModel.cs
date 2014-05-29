using System.Windows.Input;

namespace Olf.GoldenHorse.Foundation.ViewModels
{
    public interface IMainShellViewModel
    {
        ICommand NewProjectSuiteCommand { get; }
        ICommand OpenProjectSuiteCommand { get; } 
    }
}