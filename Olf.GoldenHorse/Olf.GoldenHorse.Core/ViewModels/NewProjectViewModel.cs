using System;
using System.IO;
using System.Windows.Forms;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Olf.GoldenHorse.Core.Controllers;
using Olf.GoldenHorse.Foundation.Controllers;
using Olf.GoldenHorse.Foundation.ViewModels;

namespace Olf.GoldenHorse.Core.ViewModels
{
    public class NewProjectViewModel : ViewModelBase, INewProjectViewModel
    {
        private readonly IProjectController projectController;
        private string location;
        private DelegateCommand saveNewProjectCommand;
        private string name;

        public string Name
        {
            get { return name; }
            set
            {
                name = value; 
                
                saveNewProjectCommand.RaiseCanExecuteChanged();
            }
        }

        public string Location
        {
            get { return location; }
            set
            {
                location = value;
                
                OnPropertyChanged("Location");

                saveNewProjectCommand.RaiseCanExecuteChanged();
            }
        }

        public ICommand BrowseCommand { get; protected set; }
        public ICommand SaveNewProjectCommand { get; protected set; }
        public ICommand CancelNewProjectCommand { get; protected set; }

        public NewProjectViewModel(IProjectController projectController)
        {
            this.projectController = projectController;
            BrowseCommand = new DelegateCommand(ExecuteBrowseCommand);

            saveNewProjectCommand = new DelegateCommand(ExecuteSaveNewProjectCommandCommand, CanExecuteSaveNewProjectCommandCommand);
            SaveNewProjectCommand = saveNewProjectCommand;

            CancelNewProjectCommand = new DelegateCommand(ExecuteCancelNewProjectCommand);

            Location = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "GoldenHorseProjects");
        }

        protected virtual void ExecuteCancelNewProjectCommand()
        {
            throw new NotImplementedException();
        }

        protected virtual bool CanExecuteSaveNewProjectCommandCommand()
        {
            return !(string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Location));
        }

        protected virtual void ExecuteSaveNewProjectCommandCommand()
        {
            projectController.Create(Location, Name);
        }

        protected virtual void ExecuteBrowseCommand()
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();

            DialogResult dialogResult = folderBrowserDialog.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                Location = folderBrowserDialog.SelectedPath;
            }
        }
    }
}