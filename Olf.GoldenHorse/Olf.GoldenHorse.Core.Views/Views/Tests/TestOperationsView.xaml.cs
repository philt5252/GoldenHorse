using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
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

namespace Olf.GoldenHorse.Core.Views
{
    /// <summary>
    /// Interaction logic for TestOperationsView.xaml
    /// </summary>
    public partial class TestOperationsView : UserControl, IViewWithDataContext
    {
        private Stopwatch stopwatch;
        private int count;
        public TestOperationsView()
        {
            InitializeComponent();
            count = 0;
        }

        private void DoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (stopwatch != null && stopwatch.ElapsedMilliseconds > 600)
            {
                count = 0;
            }
            if (count == 0)
            {
                stopwatch = Stopwatch.StartNew();
                count++;
            }
            else if (count == 1)
            {
                int elapsedMilliseconds = (int)stopwatch.ElapsedMilliseconds;
                count = 0;
                if (elapsedMilliseconds <= (int)GetDoubleClickTime())
                {
                    
                 //AddToTestCommand
                }
            }
        }

        [DllImport("user32.dll")]
        static extern uint GetDoubleClickTime();


        private void HandleRequestBringIntoView(object sender, RequestBringIntoViewEventArgs e)
        {
            e.Handled = true;
        }
    }
}
