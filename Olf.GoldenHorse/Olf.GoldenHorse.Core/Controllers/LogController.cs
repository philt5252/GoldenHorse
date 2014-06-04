using Microsoft.Practices.Prism.Regions;
using Olf.GoldenHorse.Core.Helpers;
using Olf.GoldenHorse.Foundation.Controllers;
using Olf.GoldenHorse.Foundation.Factories.ViewModels;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.ViewModels;
using Olf.GoldenHorse.Foundation.Views;
using Olf.GoldenHorse.Foundation.Views.Factories;

namespace Olf.GoldenHorse.Core.Controllers
{
    public class LogController : ILogController
    {
        private readonly ILogMainShellViewModelFactory logMainShellViewModelFactory;
        private readonly ILogMainShellViewFactory logMainShellViewFactory;
        private readonly IRegionManager regionManager;

        public LogController(ILogMainShellViewModelFactory logMainShellViewModelFactory,
            ILogMainShellViewFactory logMainShellViewFactory,
            IRegionManager regionManager)
        {
            this.logMainShellViewModelFactory = logMainShellViewModelFactory;
            this.logMainShellViewFactory = logMainShellViewFactory;
            this.regionManager = regionManager;
        }

        public void ShowLog(Log log)
        {
            ILogMainShellViewModel logMainShellViewModel = logMainShellViewModelFactory.Create(log);
            IViewWithDataContext logMainShellView = logMainShellViewFactory.Create();

            logMainShellView.DataContext = logMainShellViewModel;

            regionManager.Regions[Regions.WorkspaceViewRegion].AddAndActivate(logMainShellView);
        }
    }
}