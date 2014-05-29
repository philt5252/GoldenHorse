using System.Diagnostics;
using System.Windows;

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
