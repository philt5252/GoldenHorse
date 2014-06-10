using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Xceed.Wpf.Toolkit;

namespace Olf.GoldenHorse.Core.Views.Views.Variables
{
    /// <summary>
    /// Interaction logic for VariableTableWindow.xaml
    /// </summary>
    public partial class VariableTableWindow : WindowBase
    {
        public VariableTableWindow()
        {
            InitializeComponent();
        }

        private void ColumnsUpDown_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            IntegerUpDown integerUpDown = sender as IntegerUpDown;

            dataGrid.Columns.Add(new DataGridTemplateColumn()); 
        }
    }
}
