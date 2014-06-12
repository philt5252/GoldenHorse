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
using EditBlockTest;
using Olf.GoldenHorse.Foundation.ViewModels.Nodes;
using Olf.GoldenHorse.Foundation.Views;

namespace Olf.GoldenHorse.Core.Views
{
    /// <summary>
    /// Interaction logic for ProjectExplorerView.xaml
    /// </summary>
    public partial class ProjectExplorerView : UserControl, IViewWithDataContext
    {
        private Stopwatch stopwatch;
        private Stopwatch stopwatch2;
        private EditableTextBlock previousTextBlock;
        private int count;
        private int clickCount;
        public ProjectExplorerView()
        {
            InitializeComponent();
            count = 0;
            clickCount = 0;
        }

        private void DoubleClick(object sender, MouseButtonEventArgs e)
        {


            if (stopwatch != null && stopwatch.ElapsedMilliseconds > 600)
            {
                count = 0;
            }
            if (e.ClickCount==2)
            {

                    IDisplayNode displayNode = projectExplorerTvw.SelectedItem as IDisplayNode;
                    if (displayNode.DefaultCommand == null)
                    {
                        return;
                    }
                    displayNode.DefaultCommand.Execute(null);
                
            }
        }

        [DllImport("user32.dll")]
        static extern uint GetDoubleClickTime();


        private void HandleRequestBringIntoView(object sender, RequestBringIntoViewEventArgs e)
        {
            e.Handled = true;
        }

        private void EditTextBlockName(object sender, MouseButtonEventArgs e)
        {
            EditableTextBlock editableTextBlock = sender as EditableTextBlock;
            if (previousTextBlock == null || previousTextBlock != editableTextBlock)
            {
                previousTextBlock = editableTextBlock;
                clickCount = 0;
            }
            if (e.MiddleButton == MouseButtonState.Pressed || e.RightButton == MouseButtonState.Pressed)
            {
                editableTextBlock.IsInEditMode = true;
            }
            else if (clickCount == 0)
            {
                clickCount++;
                stopwatch2 = Stopwatch.StartNew();
            }
            else if (clickCount == 1)
            {
                int elapsedMilliseconds = (int)stopwatch2.ElapsedMilliseconds;
                if (elapsedMilliseconds > 600)
                {
                    editableTextBlock.IsInEditMode = true;
                    stopwatch2.Reset();
                    clickCount = 0;
                }
            }

        }
    }
}
