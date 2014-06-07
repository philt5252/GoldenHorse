
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
        private readonly ITestItemController testItemController;
        private readonly IRecordingController recordingController;
        private ITabItemViewModel[] tabItems;
        private int selectedTabIndex = 0;
        private DelegateCommand nextCommand;

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
        
            tabItems = new ITabItemViewModel[]
            {
                TestObjectEditorViewModel,
                TestOperationEditorViewModel,
                TestParameterEditorViewModel,
                TestDescriptionEditorViewModel
            };

            FinishCommand = new DelegateCommand(ExecuteFinishCommand);
            nextCommand = new DelegateCommand(ExecuteNextCommand, CanExeccuteNextCommand);
            NextCommand = nextCommand;
        }

        private bool CanExeccuteNextCommand()
        {
            return selectedTabIndex < tabItems.Length - 1;
        }

        private void ExecuteNextCommand()
        {
            selectedTabIndex++;
            nextCommand.RaiseCanExecuteChanged();
            tabItems[selectedTabIndex].IsSelected = true;
            tabItems[selectedTabIndex-1].IsSelected = false;
        }

        protected virtual void ExecuteFinishCommand()
        {
            testItem.Test.TestItems.Add(testItem);
            testItemController.CloseTestItemEditorWindow();
            recordingController.ResumeRecorder();
        }
    }
}