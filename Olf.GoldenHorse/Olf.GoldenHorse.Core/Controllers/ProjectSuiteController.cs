using System;
using System.Windows.Forms;
using Microsoft.Practices.Prism.Regions;
using Olf.GoldenHorse.Core.Helpers;
using Olf.GoldenHorse.Foundation;
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
        private readonly IProjectExplorerViewFactory projectExplorerViewFactory;
        private readonly IProjectExplorerViewModelFactory projectExplorerViewModelFactory;
        private readonly IProjectSuiteFileManager projectSuiteFileManager;
        private readonly IRegionManager regionManager;
        private IWindow newProjectSuiteWindow;

        public ProjectSuiteController(INewProjectSuiteWindowFactory newProjectSuiteWindowFactory,
            INewProjectSuiteViewModelFactory newProjectSuiteViewModelFactory,
            IProjectExplorerViewFactory projectExplorerViewFactory,
            IProjectExplorerViewModelFactory projectExplorerViewModelFactory,
            IProjectSuiteFileManager projectSuiteFileManager,
            IRegionManager regionManager)
        {
            this.newProjectSuiteWindowFactory = newProjectSuiteWindowFactory;
            this.newProjectSuiteViewModelFactory = newProjectSuiteViewModelFactory;
            this.projectExplorerViewFactory = projectExplorerViewFactory;
            this.projectExplorerViewModelFactory = projectExplorerViewModelFactory;
            this.projectSuiteFileManager = projectSuiteFileManager;
            this.regionManager = regionManager;
        }

        public void New()
        {
            newProjectSuiteWindow = newProjectSuiteWindowFactory.Create();
            INewProjectSuiteViewModel newProjectSuiteViewModel = newProjectSuiteViewModelFactory.Create();

            newProjectSuiteWindow.DataContext = newProjectSuiteViewModel;

            newProjectSuiteWindow.ShowDialog();
        }

        public void Create(string folderPath, string projectSuiteName)
        {
            ProjectSuite projectSuite = new ProjectSuite();
            projectSuite.ProjectSuiteFolder = folderPath;
            projectSuite.Name = projectSuiteName;

            ProjectSuiteManager.CurrentProjectSuite = projectSuite;

            Project project = new Project();
            project.Name = projectSuiteName;
            project.IsDefaultProject = true;

            projectSuite.Projects.Add(project);

            projectSuiteFileManager.Create(projectSuite);

            CloseNewProjectSuiteWindow();

            ResetForNewProjectSuite();
        }

        private void ResetForNewProjectSuite()
        {
            IProjectExplorerViewModel projectExplorerViewModel = projectExplorerViewModelFactory.Create();

            IViewWithDataContext view = regionManager.Regions[Regions.ExplorerViewRegion].GetView(ViewNames.ProjectExplorerView)
                as IViewWithDataContext;

            view.DataContext = projectExplorerViewModel;
        }

        public void Open()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            string extension = "*" + DefaultData.ProjectSuiteExtension;
            openFileDialog.Filter = string.Format("GH Project Suite ({0})|{0}", extension);
            openFileDialog.InitialDirectory = DefaultData.GoldenHorseProjectsLocation;

            DialogResult dialogResult = openFileDialog.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                ProjectSuite projectSuite = projectSuiteFileManager.Open(openFileDialog.FileName);
                ProjectSuiteManager.CurrentProjectSuite = projectSuite;
                ResetForNewProjectSuite();
            }
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