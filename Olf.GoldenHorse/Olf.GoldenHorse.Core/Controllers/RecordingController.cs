using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using MouseKeyboardActivityMonitor;
using MouseKeyboardActivityMonitor.WinApi;
using Olf.GoldenHorse.Core.Models;
using Olf.GoldenHorse.Core.Services;
using Olf.GoldenHorse.Foundation.Controllers;
using Olf.GoldenHorse.Foundation.DataAccess;
using Olf.GoldenHorse.Foundation.Factories.Services;
using Olf.GoldenHorse.Foundation.Factories.ViewModels;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.Services;
using Olf.GoldenHorse.Foundation.ViewModels;
using Olf.GoldenHorse.Foundation.Views;
using Olf.GoldenHorse.Foundation.Views.Factories;
using TestStack.White.UIItems;
using Application = System.Windows.Application;

namespace Olf.GoldenHorse.Core.Controllers
{
    public class RecordingController : IRecordingController
    {
        private readonly IRecordWindowFactory recordWindowFactory;
        private readonly IRecorderViewModelFactory recorderViewModelFactory;
        private readonly IRecorderFactory recorderFactory;
        private readonly ITestFileManager testFileManager;
        private readonly IProjectFileManager projectFileManager;
        private readonly IAppController appController;
        private readonly ITestController testController;
        private readonly ITestItemController testItemController;
        private IWindow recordingWindow;
        private IRecorder recorder;

        public RecordingController(IRecordWindowFactory recordWindowFactory,
            IRecorderViewModelFactory recorderViewModelFactory,
            IRecorderFactory recorderFactory,
            ITestFileManager testFileManager,
            IProjectFileManager projectFileManager, 
            IAppController appController,
            ITestController testController,
            ITestItemController testItemController)
        {
            this.recordWindowFactory = recordWindowFactory;
            this.recorderViewModelFactory = recorderViewModelFactory;
            this.recorderFactory = recorderFactory;
            this.testFileManager = testFileManager;
            this.projectFileManager = projectFileManager;
            this.appController = appController;
            this.testController = testController;
            this.testItemController = testItemController;
        }

        public void ShowRecord()
        {
            Project defaultProject = ProjectSuiteManager.GetDefaultProject();

            Test test = testFileManager.CreateTestForProject(defaultProject);

            ShowRecordingWindow(test);
        }

        private void ShowRecordingWindow(Test test)
        {
            recordingWindow = recordWindowFactory.Create();

            recorder = recorderFactory.Create(test);
            IRecorderViewModel recorderViewModel = recorderViewModelFactory.Create(recorder);

            recordingWindow.DataContext = recorderViewModel;

            //appController.MainWindow.Minimize();

            recordingWindow.Show();
            appController.MainWindow.Minimize();
        }

        public void StopRecord()
        {
            if (recorder != null)
                recorder.Stop();

            recordingWindow.Close();

            testFileManager.Save(recorder.CurrentTest);
            projectFileManager.Save(recorder.CurrentTest.Project);

            appController.MainWindow.Restore();
            testController.ShowTest(recorder.CurrentTest);

            recorder = null;
        }

        public void DoValidation(TestItem onScreenValidation)
        {
            onScreenValidation.Test = recorder.CurrentTest;
            testItemController.EditTestItem(onScreenValidation);
        }

        public void PauseRecord()
        {
            if (recorder != null)
                recorder.Pause();
        }

        public void ResumeRecorder()
        {
            if(recorder != null)
                recorder.Record();
        }

        public void AppendToTest(Test test)
        {
            ShowRecordingWindow(test);
        }

        public void AppendToStart(Test test)
        {
            ShowRecordingWindow(test);
            recorder.InsertPosition = 0;
        }

        public void AppendAtIndex(Test test, int index)
        {
            ShowRecordingWindow(test);
            recorder.InsertPosition = index;
        }
    }
}