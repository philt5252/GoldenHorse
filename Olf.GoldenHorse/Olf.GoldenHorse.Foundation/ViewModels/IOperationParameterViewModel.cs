

using System.Windows.Input;

namespace Olf.GoldenHorse.Foundation.ViewModels
{
    public interface IOperationParameterViewModel
    {
        string Name { get; }
        string Value { get; }
        ICommand EditParameterCommand { get; }
    }
}