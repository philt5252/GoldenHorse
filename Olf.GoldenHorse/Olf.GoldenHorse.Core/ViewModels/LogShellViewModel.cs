
using Olf.GoldenHorse.Foundation.Factories.ViewModels;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.ViewModels;

namespace Olf.GoldenHorse.Core.ViewModels
{
    public class LogShellViewModel : ILogShellViewModel
    {
        public ILogDetailsViewModel LogDetailsViewModel { get; protected set; }

        public LogShellViewModel(Log log, ILogDetailsViewModelFactory logDetailsViewModelFactory)
        {
            LogDetailsViewModel = logDetailsViewModelFactory.Create(log);
        }
    }
}