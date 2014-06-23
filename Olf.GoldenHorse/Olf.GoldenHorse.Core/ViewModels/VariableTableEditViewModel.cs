
using System;
using System.Data;
using System.IO;
using System.Windows.Input;
using Excel;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Win32;
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
        public ICommand ImportCommand { get; protected set; }
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
            ImportCommand = new DelegateCommand(ExecuteImportCommand);
            dataTable = variable.DataTableValue ?? new DataTable("DataTableName");
        }

        private void ExecuteImportCommand()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;

            dialog.Filter = "Excel Files (*.xls, *.xlsx)|*.xls;*.xlsx";

            bool? showDialog = dialog.ShowDialog();

            if (!showDialog.Value)
            {
                return;
            }

            FileStream reader = new FileStream(dialog.FileName, FileMode.Open, FileAccess.Read);

            IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(reader);

            excelReader.IsFirstRowAsColumnNames = true;
            DataSet result = excelReader.AsDataSet();

            Variables = result.Tables[0].AsDataView();

            excelReader.Close();
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