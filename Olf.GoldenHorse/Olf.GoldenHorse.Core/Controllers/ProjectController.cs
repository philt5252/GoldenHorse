using System.IO;
using Microsoft.Practices.Prism.Regions;
using Olf.GoldenHorse.Foundation.Controllers;
using Olf.GoldenHorse.Foundation.DataAccess;
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
        private readonly INewProjectSuiteViewModelFactory newProjectSuiteViewModelFactory;
        private readonly IProjectFileManager projectFileManager;
        private readonly IRegionManager regionManager;
        private IWindow newProjectWindow;

        public ProjectController(INewProjectWindowFactory newProjectWindowFactory,
            INewProjectSuiteViewModelFactory newProjectSuiteViewModelFactory,
            IProjectFileManager projectFileManager,
            IRegionManager regionManager)
        {
            this.newProjectWindowFactory = newProjectWindowFactory;
            this.newProjectSuiteViewModelFactory = newProjectSuiteViewModelFactory;
            this.projectFileManager = projectFileManager;
            this.regionManager = regionManager;
        }

        public void New()
        {
            newProjectWindow = newProjectWindowFactory.Create();
            INewProjectSuiteViewModel newProjectSuiteViewModel = newProjectSuiteViewModelFactory.Create();

            newProjectWindow.DataContext = newProjectSuiteViewModel;

            newProjectWindow.Show();
        }

        public void Open()
        {
            Project project = new Project();
            ProjectSuiteManager.AddProject(project);
        }

        public void Create(string projectPath, string projectName)
        {
            CloseNewProjectWindow();

            Project project = new Project();
            project.Name = projectName;

            ProjectSuiteManager.AddProject(project);

            projectFileManager.Create(project);
                
        }

        public void CancelNew()
        {
            CloseNewProjectWindow();
        }

        private void CloseNewProjectWindow()
        {
            if (newProjectWindow != null)
            {
                newProjectWindow.Close();
                newProjectWindow = null;
            }
        }
    }
}