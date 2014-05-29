using System;
using System.Windows.Forms;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Olf.GoldenHorse.Foundation.ViewModels;

namespace Olf.GoldenHorse.Core.ViewModels
{
    public class NewProjectViewModel : ViewModelBase, INewProjectViewModel
    {
        private string location;
        private DelegateCommand saveCommand;
        private string name;

        public string Name
        {
            get { return name; }
            set
            {
                name = value; 
                
                saveCommand.RaiseCanExecuteChanged();
            }
        }

        public string Location
        {
            get { return location; }
            set
            {
                location = value;
                
                OnPropertyChanged("Location");

                saveCommand.RaiseCanExecuteChanged();
            }
        }

        public ICommand BrowseCommand { get; protected set; }
        public ICommand SaveCommand { get; protected set; }
        public ICommand CancelCommand { get; protected set; }

        public NewProjectViewModel()
        {
            BrowseCommand = new DelegateCommand(ExecuteBrowseCommand);

            saveCommand = new DelegateCommand(ExecuteSaveCommand, CanExecuteSaveCommand);
            SaveCommand = saveCommand;

            CancelCommand = new DelegateCommand(ExecuteCancelCommand);
        }

        protected virtual void ExecuteCancelCommand()
        {
            throw new NotImplementedException();
        }

        protected virtual bool CanExecuteSaveCommand()
        {
            throw new NotImplementedException();
        }

        protected virtual void ExecuteSaveCommand()
        {
            throw new NotImplementedException();
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