
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Olf.GoldenHorse.Foundation.Controllers;
using Olf.GoldenHorse.Foundation.Factories.ViewModels;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.ViewModels;

namespace Olf.GoldenHorse.Core.ViewModels
{
    public class TestItemEditorViewModel : ITestItemEditorViewModel
    {
        private readonly TestItem testItem;
        private readonly IRecordingController recordingController;
        public ITestObjectEditorViewModel TestObjectEditorViewModel { get; protected set; }
        public ITestOperationEditorViewModel TestOperationEditorViewModel { get; protected set; }
        public ITestParameterEditorViewModel TestParameterEditorViewModel { get; protected set; }
        public ITestDescriptionEditorViewModel TestDescriptionEditorViewModel { get; protected set; }

        public ICommand CancelCommand { get; protected set; }
        public ICommand NextCommand { get; protected set; }
        public ICommand FinishCommand { get; protected set; }

        public TestItemEditorViewModel(TestItem testItem,
            ITestObjectEditorViewModelFactory testObjectEditorViewModelFactory,
            ITestOperationEditorViewModelFactory testOperationEditorViewModelFactory,
            ITestParameterEditorViewModelFactory testParameterEditorViewModelFactory,
            ITestDescriptionEditorViewModelFactory testDescriptionEditorViewModelFactory,
            IRecordingController recordingController)
        {
            this.testItem = testItem;
            this.recordingController = recordingController;

            TestObjectEditorViewModel = testObjectEditorViewModelFactory.Create(testItem);
            TestOperationEditorViewModel = testOperationEditorViewModelFactory.Create(testItem);
            TestParameterEditorViewModel = testParameterEditorViewModelFactory.Create(testItem);
            TestDescriptionEditorViewModel = testDescriptionEditorViewModelFactory.Create(testItem);
        
            FinishCommand = new DelegateCommand(ExecuteFinishCommand);
        }

        protected virtual void ExecuteFinishCommand()
        {
            testItem.Test.TestItems.Add(testItem);
            recordingController.ResumeRecorder();
        }
    }
}