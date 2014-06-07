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

namespace Olf.GoldenHorse.Core.Views.Views.Tests
{
    /// <summary>
    /// Interaction logic for TestItemEditorWindow.xaml
    /// </summary>
    public partial class TestItemEditorWindow : WindowBase
    {
        public TestItemEditorWindow()
        {
            InitializeComponent();
            DataContextChanged += OnDataContextChanged;
        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            ITestItemEditorViewModel testItemEditorViewModel = DataContext as ITestItemEditorViewModel;

            testObjectEditorView.DataContext = testItemEditorViewModel.TestObjectEditorViewModel;
            testOperationEditorView.DataContext = testItemEditorViewModel.TestOperationEditorViewModel;
            testParameterEditorView.DataContext = testItemEditorViewModel.TestParameterEditorView;
            testDescriptionEditorView.DataContext = testItemEditorViewModel.TestDescriptionEditorViewModel;
        }
    }
}
