using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Olf.GoldenHorse.Foundation.ViewModels;
using Olf.GoldenHorse.Foundation.Views;

namespace Olf.GoldenHorse.Core.Views.Views.Variables
{
    /// <summary>
    /// Interaction logic for VariableTableEditView.xaml
    /// </summary>
    public partial class VariableTableEditView : UserControl, IViewWithDataContext
    {
        private IVariableTableEditViewModel variableTableEditViewModel
        {
            get
            {
                return DataContext as IVariableTableEditViewModel;
            }
        }

        public VariableTableEditView()
        {
            InitializeComponent();
        }


        private void MenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            ContextMenu contextMenu = menuItem.Parent as ContextMenu;
            DataGridColumnHeader dataGridColumnHeader = contextMenu.PlacementTarget as DataGridColumnHeader;
            string colName = dataGridColumnHeader.Content.ToString();

            variableTableEditViewModel.DeleteColumn(colName);
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            StackPanel panel = button.Parent as StackPanel;
            TextBox textBox = panel.Children.OfType<TextBox>().First();

            MenuItem menuItem = panel.Parent as MenuItem;
            ContextMenu contextMenu = menuItem.Parent as ContextMenu;
            DataGridColumnHeader dataGridColumnHeader = contextMenu.PlacementTarget as DataGridColumnHeader;

            string origColName = dataGridColumnHeader.Content.ToString();
            variableTableEditViewModel.RenameColumn(origColName, textBox.Text);

        }
    }

}
