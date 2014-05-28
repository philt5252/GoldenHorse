using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace TestForGolden
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel mainWindowViewModel;

        public MainWindow()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            InitializeComponent();

            mainWindowViewModel = new MainWindowViewModel();

            DataContext = mainWindowViewModel;
            sw.Stop();
        }

        private void TreeView_OnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            mainWindowViewModel.SelectedTestItemViewModel = treeView1.SelectedItem as ITestItemViewModel;
        }
    }
}
