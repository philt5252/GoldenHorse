using Olf.GoldenHorse.Foundation.Views;

namespace Olf.GoldenHorse.Foundation.Controllers
{
    public interface IAppController
    {
        void Home();
        IWindow MainWindow { get; }
    }
}