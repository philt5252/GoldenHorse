using System;
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
    public class ProjectSuiteController : IProjectSuiteController
    {
        private readonly INewProjectSuiteWindowFactory newProjectSuiteWindowFactory;
        private readonly INewProjectSuiteViewModelFactory newProjectSuiteViewModelFactory;
        private readonly IProjectSuiteFileManager projectSuiteFileManager;
        private readonly IRegionManager regionManager;
        private IWindow newProjectSuiteWindow;

        public ProjectSuiteController(INewProjectSuiteWindowFactory newProjectSuiteWindowFactory,
            INewProjectSuiteViewModelFactory newProjectSuiteViewModelFactory,
            IProjectSuiteFileManager projectSuiteFileManager,
            IRegionManager regionManager)
        {
            this.newProjectSuiteWindowFactory = newProjectSuiteWindowFactory;
            this.newProjectSuiteViewModelFactory = newProjectSuiteViewModelFactory;
            this.projectSuiteFileManager = projectSuiteFileManager;
            this.regionManager = regionManager;
        }

        public void New()
        {
            newProjectSuiteWindow = newProjectSuiteWindowFactory.Create();
            INewProjectSuiteViewModel newProjectSuiteViewModel = newProjectSuiteViewModelFactory.Create();

            newProjectSuiteWindow.DataContext = newProjectSuiteViewModel;

            newProjectSuiteWindow.Show();
        }

        public void Create(string folderPath, string projectSuiteName)
        {
            ProjectSuite projectSuite = new ProjectSuite();
            projectSuite.ProjectSuiteFolder = folderPath;
            projectSuite.Name = projectSuiteName;

            ProjectSuiteManager.CurrentProjectSuite = projectSuite;

            Project project = new Project();
            project.Name = projectSuiteName;

            projectSuite.Projects.Add(project);

            projectSuiteFileManager.Create(projectSuite);

            CloseNewProjectSuiteWindow();
        }

        public void Open()
        {
            throw new NotImplementedException();
        }

        public void CancelNew()
        {
            CloseNewProjectSuiteWindow();
        }

        private void CloseNewProjectSuiteWindow()
        {
            if (newProjectSuiteWindow != null)
            {
                newProjectSuiteWindow.Close();
                newProjectSuiteWindow = null;
            }
        }
    }
}