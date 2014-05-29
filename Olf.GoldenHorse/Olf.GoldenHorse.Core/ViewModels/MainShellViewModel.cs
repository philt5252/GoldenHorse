
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
        private readonly IProjectController projectController;
        public ICommand NewProjectCommand { get; protected set; }

        public MainShellViewModel(IProjectController projectController)
        {
            this.projectController = projectController;
            NewProjectCommand = new DelegateCommand(ExecuteNewProjectCommand);
        }

        protected virtual void ExecuteNewProjectCommand()
        {
            projectController.New();
        }
    }
}