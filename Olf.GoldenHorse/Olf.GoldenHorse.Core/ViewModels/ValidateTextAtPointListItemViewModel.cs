using System.Windows.Input;
using Olf.GoldenHorse.Foundation.Controllers;
using Olf.GoldenHorse.Foundation.ViewModels;

namespace Olf.GoldenHorse.Core.ViewModels
{
    public class ValidateTextAtPointListItemViewModel : ValidationListItemViewModel
    {
        public ValidateTextAtPointListItemViewModel(IRecordingController recordingController)
            : base(recordingController)
        {
        }
    }
}