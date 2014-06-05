
using System;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Olf.GoldenHorse.Foundation.Controllers;
using Olf.GoldenHorse.Foundation.DataAccess;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.Services;
using Olf.GoldenHorse.Foundation.ViewModels;

namespace Olf.GoldenHorse.Core.ViewModels
{
    public class MainShellViewModel : IMainShellViewModel
    {
        private readonly IProjectSuiteController projectSuiteController;
        private readonly IRecordingController recordingController;
        private readonly ITestFileManager testFileManager;
        public ICommand NewProjectSuiteCommand { get; protected set; }
        public ICommand OpenProjectSuiteCommand { get; protected set; }
        public ICommand RecordCommand { get; protected set; }

        public MainShellViewModel(IProjectSuiteController projectSuiteController,
            IRecordingController recordingController, ITestFileManager testFileManager)
        {
            this.projectSuiteController = projectSuiteController;
            this.recordingController = recordingController;
            this.testFileManager = testFileManager;
            NewProjectSuiteCommand = new DelegateCommand(ExecuteNewProjectSuiteCommand);
            OpenProjectSuiteCommand = new DelegateCommand(ExecuteOpenProjectSuiteCommand);
            RecordCommand = new DelegateCommand(ExecuteRecordCommand, CanExecuteRecordCommand);
        }

        private bool CanExecuteRecordCommand()
        {
            return true;
        }

        private void ExecuteRecordCommand()
        {
            recordingController.ShowRecord();
        }

        private void ExecuteOpenProjectSuiteCommand()
        {
            projectSuiteController.ShowOpen();
        }

        protected virtual void ExecuteNewProjectSuiteCommand()
        {
            projectSuiteController.New();
        }
    }
}