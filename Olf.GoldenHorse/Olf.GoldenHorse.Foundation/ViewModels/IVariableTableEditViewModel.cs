

using System.Data;
using System.Windows.Input;

namespace Olf.GoldenHorse.Foundation.ViewModels
{
    public interface IVariableTableEditViewModel
    {
        ICommand AddColumnCommand { get; }
        ICommand SaveCommand { get; }
        ICommand ImportCommand { get; }
        string ColumnName { get; set; }
        DataView Variables { get; set; }
        void DeleteColumn(string colName);
        void RenameColumn(string origColName, string newColName);
    }
}