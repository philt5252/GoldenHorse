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
    /// Interaction logic for LogShellView.xaml
    /// </summary>
    public partial class LogShellView : UserControl, IViewWithDataContext
    {
        public LogShellView()
        {
            InitializeComponent();
            DataContextChanged += OnDataContextChanged;
        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            ILogShellViewModel logShellViewModel = DataContext as ILogShellViewModel;

            logDetailsView.DataContext = logShellViewModel.LogDetailsViewModel;
            //logOperationsView.DataContext = logShellViewModel.TestOperationsViewModel;
        }
    }
}
