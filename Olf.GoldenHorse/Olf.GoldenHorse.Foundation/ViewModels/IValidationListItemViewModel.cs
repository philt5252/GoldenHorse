using System.Windows.Input;

namespace Olf.GoldenHorse.Foundation.ViewModels
{
    public interface IValidationListItemViewModel
    {
         ICommand CreateValidationCommand { get; set; }
    }
}