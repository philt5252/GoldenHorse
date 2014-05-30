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
        private readonly IAppController appController;
        private IWindow recordingWindow;

        public RecordingController(IRecordWindowFactory recordWindowFactory,
            IRecorderViewModelFactory recorderViewModelFactory,
            IRecorderFactory recorderFactory,
            IAppController appController)
        {
            this.recordWindowFactory = recordWindowFactory;
            this.recorderViewModelFactory = recorderViewModelFactory;
            this.recorderFactory = recorderFactory;
            this.appController = appController;
        }

        public void ShowRecord(Test test)
        {
            recordingWindow = recordWindowFactory.Create();

            IRecorder recorder = recorderFactory.Create(test);
            IRecorderViewModel recorderViewModel = recorderViewModelFactory.Create(recorder);

            recordingWindow.DataContext = recorderViewModel;

            //appController.MainWindow.Minimize();

            recordingWindow.Show();
            appController.MainWindow.Minimize();
        }

        public void StopRecord()
        {
            recordingWindow.Close();

            appController.MainWindow.Restore();
        }
    }
}