using System.Collections.ObjectModel;
using System.Windows.Input;
using Olf.GoldenHorse.Foundation.Models;

namespace Olf.GoldenHorse.Foundation.ViewModels
{
    public interface IVariableManagerViewModel
    {
        ObservableCollection<Variable> Variables { get; }
        Variable SelectedVariable { get; set; }
        ICommand EditTableVariableCommand { get; }
    }
}