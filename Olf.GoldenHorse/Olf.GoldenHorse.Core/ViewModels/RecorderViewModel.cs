
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Expression.Interactivity.Core;
using Microsoft.Practices.Prism.Commands;
using Olf.GoldenHorse.Core.Models;
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

        public string CurrentTest
        {
            get
            {
                return recorder.CurrentTest.Name;
            }
        }

        public IValidationListItemViewModel[] ValidationList { get; protected set; }

        public ICommand RecordCommand { get; protected set; }
        public ICommand PauseCommand { get; protected set; }
        public ICommand StopCommand { get; protected set; }

        public RecorderViewModel(IRecorder recorder,
            IEnumerable<IValidationListItemViewModel> validationList,
            IRecordingController recordingController)
        {
            this.recorder = recorder;
            this.recordingController = recordingController;
            ValidationList = validationList.ToArray();

            RecordCommand = new DelegateCommand(ExecuteRecordCommand, CanExecuteRecordCommand);
            StopCommand = new DelegateCommand(ExecuteStopCommand, CanExecuteStopCommand);
            PauseCommand = new DelegateCommand(ExecutePauseCommand, CanExecutePauseCommand);
        }

        protected virtual bool CanExecutePauseCommand()
        {
            return true;
        }

        protected virtual void ExecutePauseCommand()
        {
            throw new NotImplementedException();
        }

        protected virtual bool CanExecuteStopCommand()
        {
            return true;
        }

        protected virtual void ExecuteStopCommand()
        {
            recorder.Stop();
            recordingController.StopRecord();
        }

        protected virtual bool CanExecuteRecordCommand()
        {
            return true;
        }

        protected virtual void ExecuteRecordCommand()
        {
            recorder.Record();
        }
    }
}