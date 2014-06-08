using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Forms;
using Microsoft.Practices.Prism.Commands;
using MouseKeyboardActivityMonitor;
using MouseKeyboardActivityMonitor.WinApi;
using Olf.GoldenHorse.Core.Services;
using Olf.GoldenHorse.Foundation.Controllers;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.Services;
using Olf.GoldenHorse.Foundation.ViewModels;
using TestStack.White.UIItems;
using Application = System.Windows.Application;
using Brushes = System.Windows.Media.Brushes;
using Rectangle = System.Windows.Shapes.Rectangle;

namespace Olf.GoldenHorse.Core.ViewModels
{
    public class GetObjectScreenSelectionViewModel : GetObjectViewModel, IGetObjectScreenSelectionViewModel
    {
        private readonly ITestItemController testItemController;

        public override string Type { get { return "Validate at point"; } }

        public override string Description 
        {
            get
            {
                return "Move the mouse to the test to be validated on screen. Press Shift+Ctrl+A to select the text.";
            }
        }

        public GetObjectScreenSelectionViewModel(ITestItemController testItemController)
        {
            this.testItemController = testItemController;
            GetObjectCommand = new DelegateCommand(ExecuteGetObjectCommand);
        }

        private void ExecuteGetObjectCommand()
        {
            testItemController.MinimizeTestItemEditorWindow();

            Window window = new Window();
            window.Height = SystemParameters.VirtualScreenHeight;
            window.Width = SystemParameters.VirtualScreenWidth;
            bool doPicture = false;

            Canvas canvas = new Canvas();
            Rectangle rectangle = new Rectangle();
            rectangle.StrokeThickness = 3;
            rectangle.Stroke = Brushes.Red;
            rectangle.Height = 0;
            rectangle.Width = 0;
            Canvas.SetTop(rectangle, 0);
            Canvas.SetLeft(rectangle, 0);
            UIItem currentUIItem = null;
            System.Drawing.Point point = new System.Drawing.Point();

            Task task = new Task(() =>
            {
                doPicture = true;
                UIItem prevUIItem = null;

                GlobalHooker hooker = new GlobalHooker();
                KeyboardHookListener listener = new KeyboardHookListener(hooker);
                listener.Enabled = true;

                listener.KeyDown += (o, args) =>
                {
                    if (args.Shift && args.Control && args.KeyCode == Keys.A)
                    {
                        doPicture = false;

                    }
                };

                while (doPicture)
                {
                    point = Cursor.Position;
                    currentUIItem = ExternalAppInfoManager.GetControl(System.Windows.Forms.Cursor.Position);
                    
                    if (currentUIItem.AutomationElement.Current.ProcessId == Process.GetCurrentProcess().Id)
                        currentUIItem = prevUIItem;

                    if (currentUIItem == null)
                        continue;

                    Rect bounds = currentUIItem.AutomationElement.Current.BoundingRectangle;

                    Thread.Sleep(250);
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        rectangle.Width = bounds.Width;
                        rectangle.Height = bounds.Height;
                        Canvas.SetTop(rectangle, bounds.Y);
                        Canvas.SetLeft(rectangle, bounds.X);
                    }));
                    prevUIItem = currentUIItem;
                }

            });

            canvas.Children.Add(rectangle);
            window.Left = 0;
            window.Top = 0;

            window.Content = canvas;
            window.WindowStyle = new WindowStyle();
            window.ShowInTaskbar = false;
            window.AllowsTransparency = true;
            window.Background = Brushes.Transparent;
            window.Topmost = true;
            window.Show();
            task.Start();

            task.ContinueWith(t =>
            {
                UIItem = currentUIItem;

                TestItem onScreenValidation = testItemController.CurrentTestItem;

                OperationParameter operationParameterX = onScreenValidation.Operation.GetParameterNamed("ClientX");
                OperationParameter operationParameterY = onScreenValidation.Operation.GetParameterNamed("ClientY");

                if (operationParameterX != null && operationParameterY != null)
                {
                    operationParameterX.Value = point.X - UIItem.Bounds.X;
                    operationParameterY.Value = point.Y - UIItem.Bounds.Y;
                }

                Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    window.Close();
                    Thread.Sleep(500);
                    testItemController.RestoreTestItemEditorWindow();
                    Thread.Sleep(500);
                    testItemController.RestoreTestItemEditorWindow();
                }));
                
            });

            window.Closed += (o, args) =>
            {
                doPicture = false;
                task.Dispose();
            };
        }
    }
}