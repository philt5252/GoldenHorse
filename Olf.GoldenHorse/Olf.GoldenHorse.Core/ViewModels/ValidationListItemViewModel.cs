using System;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Olf.GoldenHorse.Core.Models;
using Olf.GoldenHorse.Foundation.Controllers;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.ViewModels;

namespace Olf.GoldenHorse.Core.ViewModels
{
    public abstract class ValidationListItemViewModel : IValidationListItemViewModel
    {
        private readonly IRecordingController recordingController;
        public ICommand CreateValidationCommand { get; set; }

        public abstract string Name { get; }

        protected ValidationListItemViewModel(IRecordingController recordingController)
        {
            this.recordingController = recordingController;

            CreateValidationCommand = new DelegateCommand(ExecuteCreateValidationCommand);
        }

        private void ExecuteCreateValidationCommand()
        {
            OnScreenValidation onScreenValidation = CreateOnScreenValidation();
            recordingController.DoValidation(onScreenValidation);
        }

        protected abstract OnScreenValidation CreateOnScreenValidation();
    }
}