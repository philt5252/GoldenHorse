using Microsoft.Practices.Prism.Regions;
using Olf.GoldenHorse.Foundation.Controllers;
using Olf.GoldenHorse.Foundation.Factories.ViewModels;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.Services;
using Olf.GoldenHorse.Foundation.ViewModels;
using Olf.GoldenHorse.Foundation.Views;
using Olf.GoldenHorse.Foundation.Views.Factories;

namespace Olf.GoldenHorse.Core.Controllers
{
    public class ProjectController : IProjectController
    {
        private readonly INewProjectWindowFactory newProjectWindowFactory;
        private readonly INewProjectViewModelFactory newProjectViewModelFactory;
        private readonly IRegionManager regionManager;

        public ProjectController(INewProjectWindowFactory newProjectWindowFactory,
            INewProjectViewModelFactory newProjectViewModelFactory,
            IRegionManager regionManager)
        {
            this.newProjectWindowFactory = newProjectWindowFactory;
            this.newProjectViewModelFactory = newProjectViewModelFactory;
            this.regionManager = regionManager;
        }

        public void New()
        {
            Project project = new Project();
            ProjectManager.CurrentProject = project;

            IWindow newProjectWindow = newProjectWindowFactory.Create();
            INewProjectViewModel newProjectViewModel = newProjectViewModelFactory.Create();

            newProjectWindow.DataContext = newProjectViewModel;

            newProjectWindow.Show();
        }

        public void Open()
        {
            Project project = new Project();
            ProjectManager.CurrentProject = project;
        }
    }
}