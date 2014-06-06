using System;
using System.IO;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Olf.GoldenHorse.Foundation.Controllers;
using Olf.GoldenHorse.Foundation.ViewModels;

namespace Olf.GoldenHorse.Core.ViewModels
{
    public class RecentFileViewModel : IRecentFileViewModel
    {
        private readonly string filePath;
        private readonly IProjectSuiteController projectSuiteController;
        public string FileName { get; protected set; }
        public ICommand OpenFileCommand { get; protected set; }

        public RecentFileViewModel(string filePath, IProjectSuiteController projectSuiteController)
        {
            this.filePath = filePath;
            this.projectSuiteController = projectSuiteController;
            FileInfo fileInfo = new FileInfo(filePath);

            FileName = fileInfo.Name;

            OpenFileCommand = new DelegateCommand(ExecuteOpenFileCommand);
        }

        private void ExecuteOpenFileCommand()
        {
            projectSuiteController.Open(filePath);
        }
    }
}