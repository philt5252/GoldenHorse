
using System;
using System.Data;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Olf.GoldenHorse.Foundation.Controllers;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.ViewModels;

namespace Olf.GoldenHorse.Core.ViewModels
{
    public class VariableTableEditViewModel : ViewModelBase, IVariableTableEditViewModel
    {
        private readonly Variable variable;
        private readonly IVariableController variableController;
        private DataTable dataTable;
        public ICommand AddColumnCommand { get; protected set; }
        public ICommand SaveCommand { get; protected set; }
        public string ColumnName { get; set; }

        public DataView Variables
        {
            get { return dataTable.AsDataView(); }
            set
            {
                dataTable = value.ToTable();
                
                OnPropertyChanged("Variables");
            }
        }

        public VariableTableEditViewModel(Variable variable, IVariableController variableController)
        {
            this.variable = variable;
            this.variableController = variableController;

            AddColumnCommand = new DelegateCommand(ExecuteAddColumnCommand);
            SaveCommand = new DelegateCommand(ExecuteSaveCommand);

            dataTable = variable.DataTableValue ?? new DataTable("DataTableName");
        }

        private void ExecuteSaveCommand()
        {
            variable.DataTableValue = dataTable;
            variableController.CloseTableEditView();
        }

        private void ExecuteAddColumnCommand()
        {
            dataTable.Columns.Add(ColumnName);

            OnPropertyChanged("Variables");
        }
    }
}