using System.Windows.Input;

namespace Olf.GoldenHorse.Foundation.ViewModels
{
    public interface IValidationListItemViewModel
    {
        string Name { get; }
        ICommand CreateValidationCommand { get; set; }
    }
}