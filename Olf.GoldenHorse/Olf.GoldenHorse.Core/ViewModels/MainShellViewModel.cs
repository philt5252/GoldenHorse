
using System;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Olf.GoldenHorse.Foundation.Controllers;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.ViewModels;

namespace Olf.GoldenHorse.Core.ViewModels
{
    public class MainShellViewModel : IMainShellViewModel
    {
        private readonly IProjectSuiteController projectSuiteController;
        public ICommand NewProjectCommand { get; protected set; }

        public MainShellViewModel(IProjectSuiteController projectSuiteController)
        {
            this.projectSuiteController = projectSuiteController;
            NewProjectCommand = new DelegateCommand(ExecuteNewProjectCommand);
        }

        protected virtual void ExecuteNewProjectCommand()
        {
            projectSuiteController.New();
        }
    }
}