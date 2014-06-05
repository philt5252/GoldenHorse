
using System;
using System.IO;
using System.Linq;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Olf.GoldenHorse.Foundation;
using Olf.GoldenHorse.Foundation.Controllers;
using Olf.GoldenHorse.Foundation.DataAccess;
using Olf.GoldenHorse.Foundation.Factories.ViewModels;
using Olf.GoldenHorse.Foundation.Services;
using Olf.GoldenHorse.Foundation.ViewModels;

namespace Olf.GoldenHorse.Core.ViewModels
{
    public class StartPageViewModel : IStartPageViewModel
    {
        private readonly IProjectSuiteController projectSuiteController;
        private readonly IRecentFileViewModelFactory recentFileViewModelFactory;

        public string Version { get { return "2.0.0"; } }
        public IRecentFileViewModel[] RecentFiles { get; protected set; }

        public ICommand CreateNewProjectCommand { get; protected set; }
        public ICommand OpenProjectCommand { get; protected set; }

        public StartPageViewModel(IProjectSuiteController projectSuiteController,
            IRecentFileViewModelFactory recentFileViewModelFactory,
            IRecentFileManager recentFileManager)
        {
            this.projectSuiteController = projectSuiteController;
            this.recentFileViewModelFactory = recentFileViewModelFactory;
            CreateNewProjectCommand = new DelegateCommand(ExecuteNewProjectCommand);
            OpenProjectCommand = new DelegateCommand(ExecuteOpenProjecctCommand);

            string filePath = DefaultData.GoldenHorseRecentProjectsFilePath;

            if (!File.Exists(filePath))
                return;

            string[] projects = recentFileManager.GetRecentFiles();

            RecentFiles = projects.Select(recentFileViewModelFactory.Create).ToArray();
        }

        private void ExecuteOpenProjecctCommand()
        {
            projectSuiteController.ShowOpen();
        }

        private void ExecuteNewProjectCommand()
        {
            projectSuiteController.New();
        }
    }
}