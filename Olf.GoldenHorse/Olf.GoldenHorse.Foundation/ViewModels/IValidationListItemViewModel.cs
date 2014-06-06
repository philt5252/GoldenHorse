using System.Windows.Input;

namespace Olf.GoldenHorse.Foundation.ViewModels
{
    public interface IValidationListItemViewModel
    {
         ICommand CreateValidation { get; set; }
    }
}