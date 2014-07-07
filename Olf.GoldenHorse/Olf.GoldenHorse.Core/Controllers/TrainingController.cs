using Olf.GoldenHorse.Foundation.Controllers;
using Olf.GoldenHorse.Foundation.Factories.ViewModels;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.ViewModels;
using Olf.GoldenHorse.Foundation.Views;
using Olf.GoldenHorse.Foundation.Views.Factories;

namespace Olf.GoldenHorse.Core.Controllers
{
    public class TrainingController : ITrainingController
    {
        private readonly ITrainingMainViewModelFactory trainingMainViewModelFactory;
        private readonly ITrainingMainWindowFactory trainingMainWindowFactory;
        private readonly IAppController appController;
        private IWindow trainingMainWindow;

        public TrainingController(ITrainingMainViewModelFactory trainingMainViewModelFactory,
            ITrainingMainWindowFactory trainingMainWindowFactory, IAppController appController)
        {
            this.trainingMainViewModelFactory = trainingMainViewModelFactory;
            this.trainingMainWindowFactory = trainingMainWindowFactory;
            this.appController = appController;
        }

        public void Start(Test test)
        {
            trainingMainWindow = trainingMainWindowFactory.Create();
            ITrainingMainViewModel trainingMainViewModel = trainingMainViewModelFactory.Create(test);

            trainingMainWindow.DataContext = trainingMainViewModel;

            trainingMainWindow.Show();

            appController.MainWindow.Minimize();
        }

        public void Stop()
        {
            trainingMainWindow.Close();
            appController.MainWindow.Restore();
        }
    }
}