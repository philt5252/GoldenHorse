using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TreeviewDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : IWindow
    {
        public int itemCount;
        public int operationCount;
        public int valueCount;

        public Stopwatch ItemStopwatch;
        public Stopwatch OperationStopwatch;
        public Stopwatch ValueStopwatch;

        public MainWindow()
        {
            InitializeComponent();
            MainWindowViewModel mainWindowViewModel = new MainWindowViewModel();
            this.DataContext = mainWindowViewModel;
            itemCount = 0;
            valueCount = 0;
            operationCount = 0;
        }

        public void OpenItemEditWindow(object sender, MouseEventArgs args)
        {

            TextBlock textBlock = sender as TextBlock;
            IAction dataContext = textBlock.DataContext as IAction;
            if (ItemStopwatch!= null && ItemStopwatch.ElapsedMilliseconds > 600)
            {
                itemCount = 0;
            }
            if (itemCount == 0)
            {
                ItemStopwatch = Stopwatch.StartNew();
                itemCount++;
            }
            else if (itemCount == 1)
            {
                int elapsedMilliseconds = (int)ItemStopwatch.ElapsedMilliseconds;
                itemCount = 0;
                if (elapsedMilliseconds <= (int)GetDoubleClickTime())
                {
                    MessageBox.Show(new Window(), dataContext.Item);

                }

            }
        }

        [DllImport("user32.dll")]
        static extern uint GetDoubleClickTime();

        public void OpenValueEditWindow(object sender, MouseEventArgs args)
        {
            TextBlock textBlock = sender as TextBlock;
            IAction dataContext = textBlock.DataContext as IAction;
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
                    MessageBox.Show(new Window(), dataContext.Value);

                }

            }
        }
        public void OpenOperationEditWindow(object sender, MouseEventArgs args)
        {
            TextBlock textBlock = sender as TextBlock;
            IAction dataContext = textBlock.DataContext as IAction;
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
                    MessageBox.Show(new Window(), dataContext.Operation);

                }
            }
        }
    }

    class TreeViewLineConverter : IValueConverter
    {
        private TreeViewItem treeViewItem;
        
        
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            TreeViewItem item = (TreeViewItem)value;
            ItemsControl ic =  ItemsControl.ItemsControlFromItemContainer(item);

            ContentPresenter dp = item.TemplatedParent as ContentPresenter;
            GridViewRowPresenter dp2 = dp.Parent as GridViewRowPresenter;
            Border border = dp2.Parent as Border;
            treeViewItem = border.TemplatedParent as TreeViewItem;
            

            ic = ItemsControl.ItemsControlFromItemContainer(treeViewItem);

            
            return ic.ItemContainerGenerator.IndexFromContainer(treeViewItem) == ic.Items.Count - 1;
           
        }

     

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            /*TreeViewItem item = (TreeViewItem)value;
            ItemsControl ic = ItemsControl.ItemsControlFromItemContainer(item);

            ContentPresenter dp = item.TemplatedParent as ContentPresenter;
            GridViewRowPresenter dp2 = dp.Parent as GridViewRowPresenter;
            Border border = dp2.Parent as Border;
            TreeViewItem treeViewItem = border.TemplatedParent as TreeViewItem;


            if (treeViewItem.AlternationCount > 0)
            {
                return true;
            }*/
            return false;



        }

    }
}
