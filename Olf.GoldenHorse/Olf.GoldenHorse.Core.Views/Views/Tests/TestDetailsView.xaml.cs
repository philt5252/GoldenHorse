using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using Olf.GoldenHorse.Foundation.ViewModels;
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
            //detailsTlv.IsExpanded = true;

            DataContextChanged += OnDataContextChanged;
            
        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            ITestDetailsViewModel testDetailsViewModel = DataContext as ITestDetailsViewModel;

            if (testDetailsViewModel == null)
                return;

            testDetailsViewModel.PropertyChanged += TestDetailsViewModelOnPropertyChanged;
        }

        private void TestDetailsViewModelOnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            ITestDetailsViewModel testDetailsViewModel = sender as ITestDetailsViewModel;

            if (args.PropertyName == "SelectedTestItem")
            {
                detailsTlv.SelectedObject = testDetailsViewModel.SelectedTestItem;
            }
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
                    TextBlock textBlock = sender as TextBlock;
                    ITestItemViewModel testItemViewModel = textBlock.DataContext as ITestItemViewModel;
                    ICommand editObjectCommand = testItemViewModel.EditObjectCommand;
                    editObjectCommand.Execute(null);
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
                    TextBlock textBlock = sender as TextBlock;
                    ITestItemViewModel testItemViewModel = textBlock.DataContext as ITestItemViewModel;
                    ICommand editParameterCommand = testItemViewModel.EditParameterCommand; 
                    editParameterCommand.Execute(null);
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
                    TextBlock textBlock = sender as TextBlock;
                    ITestItemViewModel testItemViewModel = textBlock.DataContext as ITestItemViewModel;
                    ICommand editOperationCommand = testItemViewModel.EditOperationCommand;
                    editOperationCommand.Execute(null);

                }
            }
        }

        private void HandleRequestBringIntoView(object sender, RequestBringIntoViewEventArgs e)
        {
            e.Handled = true;
        }

        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
          
        }

        private void DetailsTlv_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            scrollViewer.ScrollToVerticalOffset(scrollViewer.VerticalOffset - e.Delta);
        }

        private void detailsTlv_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                ITestDetailsViewModel testDetailsViewModel = DataContext as ITestDetailsViewModel;

                testDetailsViewModel.DeleteSelectedItemCommand.Execute(null);
            }
        }

        private void ExpandEvent(object sender, RoutedEventArgs e)
        {
            detailsTlv.IsExpanded = true;
   

        }

        private void UnexpandEvent(object sender, RoutedEventArgs e)
        {
            detailsTlv.IsExpanded = false;
        }

        private void ExecuteSelectedAppendCommand(object sender, MouseButtonEventArgs e)
        {
            ComboBoxItem appendItem = appendCbx.SelectedItem as ComboBoxItem;
            string content = appendItem.Content as string;
            ITestDetailsViewModel testDetailsViewModel = DataContext as ITestDetailsViewModel;
            if (content.Contains("End"))
            {
              testDetailsViewModel.AppendToEndOfTestCommand.Execute(null);
                
            }
            else if (content.Contains("Beginning"))
            {
                testDetailsViewModel.AppendToStartOfTestCommand.Execute(null);
            }
            else if (content.Contains("Selected"))
            {
                testDetailsViewModel.AppendAfterSelectedItemCommand.Execute(null);
            }
                
        }
    }

    
}
