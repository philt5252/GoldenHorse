using System.Windows.Input;
using Olf.GoldenHorse.Foundation.Controllers;
using Olf.GoldenHorse.Foundation.ViewModels;

namespace Olf.GoldenHorse.Core.ViewModels
{
    public abstract class ValidationListItemViewModel : IValidationListItemViewModel
    {
        private readonly IRecordingController recordingController;
        public ICommand CreateValidationCommand { get; set; }

        protected ValidationListItemViewModel(IRecordingController recordingController)
        {
            this.recordingController = recordingController;

            
        }
    }
}