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
using Olf.GoldenHorse.Foundation.ViewModels;
using Olf.GoldenHorse.Foundation.Views;

namespace Olf.GoldenHorse.Core.Views.Views.Logs
{
    /// <summary>
    /// Interaction logic for LogMainShellView.xaml
    /// </summary>
    public partial class LogMainShellView : UserControl, IViewWithDataContext
    {
        public LogMainShellView()
        {
            InitializeComponent();
            DataContextChanged += OnDataContextChanged;
        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            ILogMainShellViewModel logMainShellViewModel = DataContext as ILogMainShellViewModel;

            logShellView.DataContext = logMainShellViewModel.LogShellViewModel;
            logScreenshotsView.DataContext = logMainShellViewModel.LogScreenshotsViewModel;
        }
    }
}
