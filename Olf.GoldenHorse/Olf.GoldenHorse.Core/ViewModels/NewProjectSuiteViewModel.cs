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
    public class NewProjectSuiteViewModel : ViewModelBase, INewProjectSuiteViewModel
    {
        private readonly IProjectSuiteController projectSuiteController;
        private string location;
        private DelegateCommand saveNewProjectSuiteCommand;
        private string name;
        private bool autoSetLocation = true;
        private bool manualSetLocation = false;

        public string Name
        {
            get { return name; }
            set
            {
                name = value; 
                
                OnPropertyChanged("Location");
                
                saveNewProjectSuiteCommand.RaiseCanExecuteChanged();
            }
        }

        public string Location
        {
            get { return manualSetLocation ? location : Path.Combine(location, Name); }
            set
            {
                location = value;

                if (!autoSetLocation)
                {
                    manualSetLocation = true;
                }
                
                OnPropertyChanged("Location");

                saveNewProjectSuiteCommand.RaiseCanExecuteChanged();
            }
        }

        public ICommand BrowseCommand { get; protected set; }
        public ICommand SaveNewProjectSuiteCommand { get; protected set; }
        public ICommand CancelNewProjectSuiteCommand { get; protected set; }

        public NewProjectSuiteViewModel(IProjectSuiteController projectSuiteController)
        {
            this.projectSuiteController = projectSuiteController;
            BrowseCommand = new DelegateCommand(ExecuteBrowseCommand);

            saveNewProjectSuiteCommand = new DelegateCommand(ExecuteSaveNewProjectSuiteCommand, CanExecuteSaveNewProjectSuiteCommand);
            SaveNewProjectSuiteCommand = saveNewProjectSuiteCommand;

            CancelNewProjectSuiteCommand = new DelegateCommand(ExecuteCancelNewProjectSuiteCommand);

            Name = "Project1";
            Location = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "GoldenHorseProjects");
            autoSetLocation = false;
        }

        protected virtual void ExecuteCancelNewProjectSuiteCommand()
        {
            projectSuiteController.CancelNew();
        }

        protected virtual bool CanExecuteSaveNewProjectSuiteCommand()
        {
            return !(string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Location));
        }

        protected virtual void ExecuteSaveNewProjectSuiteCommand()
        {
            projectSuiteController.Create(Location, Name);
        }

        protected virtual void ExecuteBrowseCommand()
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();

            DialogResult dialogResult = folderBrowserDialog.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                autoSetLocation = true;
                Location = Path.Combine(folderBrowserDialog.SelectedPath, Name);
                autoSetLocation = false;
            }
        }
    }
}