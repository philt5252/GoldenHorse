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
using Microsoft.Practices.Prism.Regions;
using Olf.GoldenHorse.Foundation.ViewModels;
using Olf.GoldenHorse.Foundation.Views;

namespace Olf.GoldenHorse.Core.Views
{
    /// <summary>
    /// Interaction logic for TestMainShellView.xaml
    /// </summary>
    public partial class TestMainShellView : UserControl, IViewWithDataContext
    {
        public TestMainShellView()
        {
            InitializeComponent();
        
            DataContextChanged += OnDataContextChanged;
        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            ITestMainShellViewModel testMainShellViewModel = DataContext as ITestMainShellViewModel;

            testShellView.DataContext = testMainShellViewModel.TestShellViewModel;
            testScreenshotsView.DataContext = testMainShellViewModel.TestScreenshotsViewModel;
            variableManagerView.DataContext = testMainShellViewModel.VariableManagerViewModel;
        }
    }
}
