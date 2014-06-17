using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.ViewModels;
using Olf.GoldenHorse.Foundation.Views;

namespace Olf.GoldenHorse.Core.Views.Views.Variables
{
    /// <summary>
    /// Interaction logic for VariableManager_View.xaml
    /// </summary>
    public partial class VariableManagerView : UserControl, IViewWithDataContext
    {
        public VariableManagerView()
        {
            InitializeComponent();
            typeColumn.ItemsSource = Enum.GetValues(typeof(VariableType)).Cast<VariableType>().ToArray();
        }

        private void EditTableVariableEvent(object sender, MouseButtonEventArgs e)
        {
            DataGrid dataGrid = sender as DataGrid;
            Variable variable = dataGrid.SelectedItem as Variable;
            if (variable.VariableType == VariableType.TableValue)
            {
                IVariableManagerViewModel variableManagerViewModel = DataContext as IVariableManagerViewModel;
                variableManagerViewModel.EditTableVariableCommand.Execute(null);
            }
        }
    }
}
