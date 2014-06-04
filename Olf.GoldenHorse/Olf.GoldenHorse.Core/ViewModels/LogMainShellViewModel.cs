
using Olf.GoldenHorse.Foundation.Factories.ViewModels;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.ViewModels;

namespace Olf.GoldenHorse.Core.ViewModels
{
    public class LogMainShellViewModel : ILogMainShellViewModel
    {
        private readonly Log log;
        public ILogScreenshotsViewModel LogScreenshotsViewModel { get; protected set; }
        public ILogShellViewModel LogShellViewModel { get; protected set; }
        public string LogName { get { return log.Name; } }

        public LogMainShellViewModel(Log log, ILogShellViewModelFactory logShellViewModelFactory,
            ILogScreenshotsViewModelFactory logScreenshotsViewModelFactory)
        {
            this.log = log;
            LogShellViewModel = logShellViewModelFactory.Create(log);
            LogScreenshotsViewModel = logScreenshotsViewModelFactory.Create(log);
        }
    }
}