using Olf.GoldenHorse.Foundation.Controllers;
using Olf.GoldenHorse.Foundation.DataAccess;
using Olf.GoldenHorse.Foundation.Factories.Services;
using Olf.GoldenHorse.Foundation.Factories.ViewModels;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.Services;
using Olf.GoldenHorse.Foundation.ViewModels;
using Olf.GoldenHorse.Foundation.Views;
using Olf.GoldenHorse.Foundation.Views.Factories;

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
        private IWindow recordingWindow;
        private IRecorder recorder;

        public RecordingController(IRecordWindowFactory recordWindowFactory,
            IRecorderViewModelFactory recorderViewModelFactory,
            IRecorderFactory recorderFactory,
            ITestFileManager testFileManager,
            IProjectFileManager projectFileManager, 
            IAppController appController,
            ITestController testController)
        {
            this.recordWindowFactory = recordWindowFactory;
            this.recorderViewModelFactory = recorderViewModelFactory;
            this.recorderFactory = recorderFactory;
            this.testFileManager = testFileManager;
            this.projectFileManager = projectFileManager;
            this.appController = appController;
            this.testController = testController;
        }

        public void ShowRecord()
        {
            Project defaultProject = ProjectSuiteManager.GetDefaultProject();

            Test test = testFileManager.CreateTestForProject(defaultProject);

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
            recordingWindow.Close();

            testFileManager.Save(recorder.CurrentTest);
            projectFileManager.Save(recorder.CurrentTest.Project);

            appController.MainWindow.Restore();
            testController.ShowTest(recorder.CurrentTest);
        }
    }
}