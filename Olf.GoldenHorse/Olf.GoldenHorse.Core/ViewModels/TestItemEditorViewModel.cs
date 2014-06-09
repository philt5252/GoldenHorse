
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Olf.GoldenHorse.Foundation.Controllers;
using Olf.GoldenHorse.Foundation.Factories.ViewModels;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.ViewModels;

namespace Olf.GoldenHorse.Core.ViewModels
{
    public class TestItemEditorViewModel : ViewModelBase, ITestItemEditorViewModel
    {
        private readonly TestItem testItem;
        private readonly ITestItemController testItemController;
        private readonly IRecordingController recordingController;
        private DelegateCommand nextCommand;
        private int selectedIndex;

        public int SelectedIndex
        {
            get { return selectedIndex; }
            set
            {
                selectedIndex = value; 
                OnPropertyChanged("SelectedIndex");
                nextCommand.RaiseCanExecuteChanged();
            }
        }

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
            ITestItemController testItemController,
            IRecordingController recordingController)
        {
            this.testItem = testItem;
            this.testItemController = testItemController;
            this.recordingController = recordingController;

            TestObjectEditorViewModel = testObjectEditorViewModelFactory.Create(testItem);
            TestOperationEditorViewModel = testOperationEditorViewModelFactory.Create(testItem);
            TestParameterEditorViewModel = testParameterEditorViewModelFactory.Create(testItem);
            TestDescriptionEditorViewModel = testDescriptionEditorViewModelFactory.Create(testItem);

            CancelCommand = new DelegateCommand(ExecuteCancelCommand);
            FinishCommand = new DelegateCommand(ExecuteFinishCommand);
            nextCommand = new DelegateCommand(ExecuteNextCommand, CanExeccuteNextCommand);
            NextCommand = nextCommand;
        }

        private void ExecuteCancelCommand()
        {
            testItemController.CloseTestItemEditorWindow();
        }

        private bool CanExeccuteNextCommand()
        {
            return SelectedIndex < 3;
        }

        private void ExecuteNextCommand()
        {
            SelectedIndex++;
            nextCommand.RaiseCanExecuteChanged();
        }

        protected virtual void ExecuteFinishCommand()
        {
            if(!testItem.Test.TestItems.Contains(testItem))
                testItem.Test.TestItems.Add(testItem);

            testItemController.CloseTestItemEditorWindow();
            recordingController.ResumeRecorder();
        }
    }
}