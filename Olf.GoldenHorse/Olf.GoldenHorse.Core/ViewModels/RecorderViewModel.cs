
using System;
using System.Windows.Input;
using Microsoft.Expression.Interactivity.Core;
using Microsoft.Practices.Prism.Commands;
using Olf.GoldenHorse.Foundation.Controllers;
using Olf.GoldenHorse.Foundation.DataAccess;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.Services;
using Olf.GoldenHorse.Foundation.ViewModels;

namespace Olf.GoldenHorse.Core.ViewModels
{
    public class RecorderViewModel : IRecorderViewModel
    {
        private readonly IRecorder recorder;
        private readonly IRecordingController recordingController;
        public string CurrentTest { get; protected set; }
        public ICommand RecordCommand { get; protected set; }
        public ICommand PauseCommand { get; protected set; }
        public ICommand StopCommand { get; protected set; }

        public RecorderViewModel(IRecorder recorder, IRecordingController recordingController)
        {
            this.recorder = recorder;
            this.recordingController = recordingController;

            RecordCommand = new DelegateCommand(ExecuteRecordCommand, CanExecuteRecordCommand);
            StopCommand = new DelegateCommand(ExecuteStopCommand, CanExecuteStopCommand);
            PauseCommand = new DelegateCommand(ExecutePauseCommand, CanExecutePauseCommand);
        }

        private bool CanExecutePauseCommand()
        {
            return true;
        }

        private void ExecutePauseCommand()
        {
            throw new NotImplementedException();
        }

        private bool CanExecuteStopCommand()
        {
            return true;
        }

        private void ExecuteStopCommand()
        {
            recorder.Stop();
            recordingController.StopRecord();
        }

        private bool CanExecuteRecordCommand()
        {
            return true;
        }

        private void ExecuteRecordCommand()
        {
            recorder.Record();
        }
    }
}