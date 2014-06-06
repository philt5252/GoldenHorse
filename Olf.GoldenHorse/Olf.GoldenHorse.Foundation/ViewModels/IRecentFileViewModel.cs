

using System.Windows.Input;

namespace Olf.GoldenHorse.Foundation.ViewModels
{
    public interface IRecentFileViewModel
    {
        string FileName { get; }
        ICommand OpenFileCommand { get; }
    }
}