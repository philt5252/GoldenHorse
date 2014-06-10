using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
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
using Olf.GoldenHorse.Foundation.ViewModels;
using Olf.GoldenHorse.Foundation.Views;

namespace Olf.GoldenHorse.Core.Views
{
    /// <summary>
    /// Interaction logic for RecordWindow.xaml
    /// </summary>
    public partial class RecordWindow : WindowBase
    {
        public RecordWindow()
        {
            InitializeComponent();
        }

        private void ExecuteValidationCommand(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            IValidationListItemViewModel validationListItemViewModel = comboBox.SelectedItem as IValidationListItemViewModel;
            ICommand validationCommand = validationListItemViewModel.CreateValidationCommand;
            validationCommand.Execute(null);
        }

        private void WindowBase_Activated_1(object sender, EventArgs e)
        {
            this.Width = System.Windows.SystemParameters.PrimaryScreenWidth;
            this.Height = System.Windows.SystemParameters.PrimaryScreenHeight;
            this.Topmost = true;
            this.Top = 0;
            this.Left = 0;
        }

        private void WindowBase_Deactivated_1(object sender, EventArgs e)
        {
            this.Topmost = true;
            this.Activate();
        }
    }
}
