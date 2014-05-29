using System.Collections.Specialized;
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
    public class AppController : IAppController
    {
        private readonly IMainWindowFactory mainWindowFactory;
        private readonly IMainShellViewFactory mainShellViewFactory;
        private readonly IMainShellViewModelFactory mainShellViewModelFactory;
        private readonly IProjectWorkspaceViewFactory projectWorkspaceViewFactory;
        private readonly IProjectExplorerViewFactory projectExplorerViewFactory;
        private readonly IWorkspaceViewFactory workspaceViewFactory;
        private readonly IRegionManager regionManager;

        public AppController(IMainWindowFactory mainWindowFactory,
            IMainShellViewFactory mainShellViewFactory,
            IMainShellViewModelFactory mainShellViewModelFactory,
            IProjectWorkspaceViewFactory projectWorkspaceViewFactory,
            IProjectExplorerViewFactory projectExplorerViewFactory,
            IWorkspaceViewFactory workspaceViewFactory,
            IRegionManager regionManager)
        {
            this.mainWindowFactory = mainWindowFactory;
            this.mainShellViewFactory = mainShellViewFactory;
            this.mainShellViewModelFactory = mainShellViewModelFactory;
            this.projectWorkspaceViewFactory = projectWorkspaceViewFactory;
            this.projectExplorerViewFactory = projectExplorerViewFactory;
            this.workspaceViewFactory = workspaceViewFactory;
            this.regionManager = regionManager;
        }

        public void Home()
        {
            IWindow window = mainWindowFactory.Create();

            IViewWithDataContext mainShellView = mainShellViewFactory.Create();
            IMainShellViewModel mainShellViewModel = mainShellViewModelFactory.Create();

            mainShellView.DataContext = mainShellViewModel;

            IViewWithDataContext projectWorkspaceView = projectWorkspaceViewFactory.Create();
            IViewWithDataContext projectExplorerView = projectExplorerViewFactory.Create();
            IViewWithDataContext workspaceView = workspaceViewFactory.Create();

            window.Show();

            regionManager.Regions[Regions.MainShellViewRegion].AddAndActivate(mainShellView);


            regionManager.Regions[Regions.MainWorkspaceViewRegion].AddAndActivate(projectWorkspaceView);


            regionManager.Regions[Regions.ExplorerViewRegion].AddAndActivate(projectExplorerView, ViewNames.ProjectExplorerView);

            regionManager.Regions[Regions.WorkspaceViewRegion].AddAndActivate(workspaceView);
        }
    }
}