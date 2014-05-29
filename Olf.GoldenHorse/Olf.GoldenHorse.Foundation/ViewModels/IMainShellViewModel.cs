using System.Windows.Input;

namespace Olf.GoldenHorse.Foundation.ViewModels
{
    public interface IMainShellViewModel
    {
        ICommand NewProjectCommand { get; } 
    }
}