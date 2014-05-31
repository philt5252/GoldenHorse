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
    public class TestController : ITestController
    {
        private readonly IRegionManager regionManager;
        private readonly ITestWorkspaceViewFactory testWorkspaceViewFactory;
        private readonly ITestMainShellViewModelFactory testMainShellViewModelFactory;

        public TestController(ITestWorkspaceViewFactory testWorkspaceViewFactory,
            ITestMainShellViewModelFactory testMainShellViewModelFactory,
            IRegionManager regionManager)
        {
            this.regionManager = regionManager;
            this.testWorkspaceViewFactory = testWorkspaceViewFactory;
            this.testMainShellViewModelFactory = testMainShellViewModelFactory;
        }

        public void ShowTest(Test test)
        {
            IViewWithDataContext testWorkspaceView = testWorkspaceViewFactory.Create();
            ITestMainShellViewModel testMainShellViewModel = testMainShellViewModelFactory.Create(test);

            testWorkspaceView.DataContext = testMainShellViewModel;

            regionManager.Regions[Regions.WorkspaceViewRegion].AddAndActivate(testWorkspaceView);
        }
    }
}