
using System.Windows.Input;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.ViewModels;

namespace Olf.GoldenHorse.Core.ViewModels
{
    public class EditParameterViewModel : IEditParameterViewModel
    {
        public OperationParameterValueMode[] ValidationModes { get; protected set; }
        public OperationParameterValueMode SelectedValidationMode { get; set; }
        public string Value { get; set; }
    }
}