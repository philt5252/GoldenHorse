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
    /// Interaction logic for TestDetailsView.xaml
    /// </summary>
    public partial class TestDetailsView : UserControl, IViewWithDataContext
    {
        public int objectCount;
        public int operationCount;
        public int valueCount;

        public Stopwatch ObjectStopwatch;
        public Stopwatch OperationStopwatch;
        public Stopwatch ValueStopwatch;

        public TestDetailsView()
        {
            InitializeComponent();
        }

        public void OpenObjectEditWindow(object sender, MouseEventArgs args)
        {

           
            if (ObjectStopwatch!= null && ObjectStopwatch.ElapsedMilliseconds > 700)
            {
                objectCount = 0;
            }
            if (objectCount == 0)
            {
                ObjectStopwatch = Stopwatch.StartNew();
                objectCount++;
            }
            else if (objectCount == 1)
            {
                int elapsedMilliseconds = (int)ObjectStopwatch.ElapsedMilliseconds;
                objectCount = 0;
                if (elapsedMilliseconds <= (int)GetDoubleClickTime())
                {
                    object o = sender;
                    object dataContext = DataContext;
                }
            }
        }

        [DllImport("user32.dll")]
        static extern uint GetDoubleClickTime();

        public void OpenValueEditWindow(object sender, MouseEventArgs args)
        {
           
            if (ValueStopwatch != null && ValueStopwatch.ElapsedMilliseconds > 600)
            {
                valueCount = 0;
            }
            if (valueCount == 0)
            {
                ValueStopwatch = Stopwatch.StartNew();
                valueCount++;
            }
            else if (valueCount == 1)
            {
                int elapsedMilliseconds = (int)ValueStopwatch.ElapsedMilliseconds;
                valueCount = 0;
                if (elapsedMilliseconds <= (int)GetDoubleClickTime())
                {
                    //MessageBox.Show(new Window(), dataContext.Value);

                }

            }
        }
        public void OpenOperationEditWindow(object sender, MouseEventArgs args)
        {
            
            if (OperationStopwatch != null && OperationStopwatch.ElapsedMilliseconds > 600)
            {
                operationCount = 0;
            }
            if (operationCount == 0)
            {
                OperationStopwatch = Stopwatch.StartNew();
                operationCount++;
            }
            else if (operationCount == 1)
            {
                int elapsedMilliseconds = (int)OperationStopwatch.ElapsedMilliseconds;
                operationCount = 0;
                if (elapsedMilliseconds <= (int)GetDoubleClickTime())
                {
                    //MessageBox.Show(new Window(), dataContext.Operation);

                }
            }
        }

    }

    
}
