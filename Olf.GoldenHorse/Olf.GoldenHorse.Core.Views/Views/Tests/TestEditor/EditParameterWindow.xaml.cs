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
using Olf.GoldenHorse.Foundation.Views;

namespace Olf.GoldenHorse.Core.Views.Views.Tests.TestEditor
{
    /// <summary>
    /// Interaction logic for EditParameterView.xaml
    /// </summary>
    public partial class EditParameterWindow : WindowBase
    {
        public EditParameterWindow()
        {
            InitializeComponent();
        }

        private void EnableValueControls(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            string selectedItem = comboBox.SelectedItem as String;
            if (selectedItem.Equals("Constant"))
            {
                valueTvw.Visibility = Visibility.Collapsed;
                valueTbk.Visibility = Visibility.Visible;
                valueTbx.Visibility = Visibility.Visible;
            }
            else if (selectedItem.Equals("Variable"))
            {
                valueTbk.Visibility = Visibility.Collapsed;
                valueTbx.Visibility = Visibility.Collapsed;
                valueTvw.Visibility = Visibility.Visible;
            }
        }
    }
}
