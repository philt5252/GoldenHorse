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
        private readonly ITestMainShellViewFactory testMainShellViewFactory;
        private readonly ITestMainShellViewModelFactory testMainShellViewModelFactory;

        public TestController(ITestMainShellViewFactory testMainShellViewFactory,
            ITestMainShellViewModelFactory testMainShellViewModelFactory,
            IRegionManager regionManager)
        {
            this.regionManager = regionManager;
            this.testMainShellViewFactory = testMainShellViewFactory;
            this.testMainShellViewModelFactory = testMainShellViewModelFactory;
        }

        public void ShowTest(Test test)
        {
            IViewWithDataContext testMainShellView = testMainShellViewFactory.Create();
            ITestMainShellViewModel testMainShellViewModel = testMainShellViewModelFactory.Create(test);

            testMainShellView.DataContext = testMainShellViewModel;

            regionManager.Regions[Regions.WorkspaceViewRegion].AddAndActivate(testMainShellView);
        }
    }
}