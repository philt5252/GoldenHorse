﻿using Microsoft.Practices.Prism.Regions;
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
        private readonly IProjectExplorerViewFactory projectExplorerViewFactory;
        private readonly IRegionManager regionManager;

        public AppController(IMainWindowFactory mainWindowFactory,
            IMainShellViewFactory mainShellViewFactory,
            IMainShellViewModelFactory mainShellViewModelFactory,
            IProjectExplorerViewFactory projectExplorerViewFactory,
            IRegionManager regionManager)
        {
            this.mainWindowFactory = mainWindowFactory;
            this.mainShellViewFactory = mainShellViewFactory;
            this.mainShellViewModelFactory = mainShellViewModelFactory;
            this.projectExplorerViewFactory = projectExplorerViewFactory;
            this.regionManager = regionManager;
        }

        public void Home()
        {
            IWindow window = mainWindowFactory.Create();

            IViewWithDataContext mainShellView = mainShellViewFactory.Create();
            IMainShellViewModel mainShellViewModel = mainShellViewModelFactory.Create();

            mainShellView.DataContext = mainShellViewModel;

            IViewWithDataContext projectExplorerView = projectExplorerViewFactory.Create();

            window.Show();

            regionManager.Regions[Regions.MainShellViewRegion].AddAndActivate(mainShellView);

            regionManager.Regions[Regions.ProjectExplorerViewRegion].AddAndActivate(projectExplorerView);
        }
    }
}