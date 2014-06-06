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
using Olf.GoldenHorse.Foundation.Views;

namespace Olf.GoldenHorse.Core.Views.Views.Tests.TestEditor
{
    /// <summary>
    /// Interaction logic for TestDescriptionEditorView.xaml
    /// </summary>
    public partial class TestDescriptionEditorView : UserControl, IViewWithDataContext
    {
        public TestDescriptionEditorView()
        {
            InitializeComponent();
        }
    }
}
