using Olf.GoldenHorse.Foundation.Views;
using Olf.GoldenHorseTraining.Foundation.Controllers;
using Olf.GoldenHorseTraining.Foundation.Views.Factories;

namespace Olf.GoldenHorseTraining.Core.Controllers
{
    public class AppController : IAppController
    {
        private readonly IMainWindowFactory mainWindowFactory;

        public AppController(IMainWindowFactory mainWindowFactory)
        {
            this.mainWindowFactory = mainWindowFactory;
        }

        public void Home()
        {
            IWindow mainWindow = mainWindowFactory.Create();

            mainWindow.Show();
        }
    }
}