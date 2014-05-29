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
        private readonly INewProjectViewModelFactory newProjectViewModelFactory;
        private readonly IProjectRepository projectRepository;
        private readonly IRegionManager regionManager;
        private IWindow newProjectWindow;

        public ProjectController(INewProjectWindowFactory newProjectWindowFactory,
            INewProjectViewModelFactory newProjectViewModelFactory,
            IProjectRepository projectRepository,
            IRegionManager regionManager)
        {
            this.newProjectWindowFactory = newProjectWindowFactory;
            this.newProjectViewModelFactory = newProjectViewModelFactory;
            this.projectRepository = projectRepository;
            this.regionManager = regionManager;
        }

        public void New()
        {
            Project project = new Project();
            ProjectManager.CurrentProject = project;

            newProjectWindow = newProjectWindowFactory.Create();
            INewProjectViewModel newProjectViewModel = newProjectViewModelFactory.Create();

            newProjectWindow.DataContext = newProjectViewModel;

            newProjectWindow.Show();
        }

        public void Open()
        {
            Project project = new Project();
            ProjectManager.CurrentProject = project;
        }

        public void Create(string projectPath, string projectName)
        {
            if (newProjectWindow != null)
            {
                newProjectWindow.Close();
                newProjectWindow = null;
            }

            Project project = new Project();
            project.ProjectFolder = Path.Combine(projectPath, projectName);
            project.Name = projectName;

            ProjectManager.CurrentProject = project;

            projectRepository.Create(project);
                
        }
    }
}