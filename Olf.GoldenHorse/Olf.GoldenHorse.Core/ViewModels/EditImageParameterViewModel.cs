using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
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
using Cursor = System.Windows.Input.Cursor;
using MessageBox = System.Windows.MessageBox;
using Point = System.Windows.Point;
using Rectangle = System.Windows.Shapes.Rectangle;

namespace Olf.GoldenHorse.Core.ViewModels
{
    public class EditImageParameterViewModel : ViewModelBase, IEditImageParameterViewModel
    {
        private readonly OperationParameter parameter;
        private readonly ITestItemController testItemController;
        private Bitmap image;

        public ICommand SaveParameterCommand { get; protected set; }
        public ICommand CancelParameterCommand { get; protected set; }
        public ICommand SelectImageCommand { get; protected set; }

        public Bitmap Image
        {
            get { return image; }
            protected set
            {
                image = value; 
                OnPropertyChanged("Image");
            }
        }

        public EditImageParameterViewModel(OperationParameter parameter,
            ITestItemController testItemController)
        {
            this.parameter = parameter;
            this.testItemController = testItemController;

            SaveParameterCommand = new DelegateCommand(ExecuteSaveParameterCommand);
            CancelParameterCommand = new DelegateCommand(ExecuteCancelParameterCommand);
            SelectImageCommand = new DelegateCommand(ExecuteSelectImageCommand);
        }

        private void ExecuteSelectImageCommand()
        {
            testItemController.MinimizeTestItemEditorWindow();

            Window window = new Window();
            window.Height = SystemParameters.VirtualScreenHeight;
            window.Width = SystemParameters.VirtualScreenWidth;
            bool doPicture = false;

            Canvas canvas = new Canvas();
            canvas.Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(100, 100,100,100));
            Rectangle rectangle = new Rectangle();
            rectangle.StrokeThickness = 2;
            rectangle.Stroke = Brushes.Red;
            rectangle.Height = 0;
            rectangle.Width = 0;
            Canvas.SetTop(rectangle, 0);
            Canvas.SetLeft(rectangle, 0);
            System.Drawing.Point point = new System.Drawing.Point();

            GlobalHooker hooker = new GlobalHooker();
            MouseHookListener listener = new MouseHookListener(hooker);

            bool isMouseDown = false;
            bool isComplete = false;
            System.Drawing.Point topLeft = new System.Drawing.Point();
            System.Drawing.Point bottomRight = new System.Drawing.Point();

            listener.MouseDown += (o, args) =>
            {
                isMouseDown = true;
                topLeft = System.Windows.Forms.Cursor.Position;
            };

            listener.MouseUp += (o, args) =>
            {

                isMouseDown = false;
                bottomRight = System.Windows.Forms.Cursor.Position;
                isComplete = true;
            };

            listener.Enabled = true;

            Task task = new Task(() =>
            {
                listener.Enabled = true;
                
                while (!isComplete)
                {
                    Thread.Sleep(250);
                    while (isMouseDown)
                    {
                        point = System.Windows.Forms.Cursor.Position;


                        Rect bounds = new Rect(new Point(topLeft.X, topLeft.Y), new Point(point.X, point.Y));

                        Thread.Sleep(100);
                        Application.Current.Dispatcher.Invoke(new Action(() =>
                        {
                            rectangle.Width = bounds.Width;
                            rectangle.Height = bounds.Height;
                            Canvas.SetTop(rectangle, bounds.Y);
                            Canvas.SetLeft(rectangle, bounds.X);
                        }));
                    }
                }

            });

            canvas.Children.Add(rectangle);
            window.Left = 0;
            window.Top = 0;

            window.Content = canvas;
            window.WindowStyle = new WindowStyle();
            window.ShowInTaskbar = false;
            window.AllowsTransparency = true;

            Bitmap screenshot = new Bitmap(1,1);

            BitmapImage bitmapImage = new BitmapImage();

            window.Topmost = true;

            Task waitTask = new Task(() =>
            {
                Thread.Sleep(3000);
                Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    using (MemoryStream memory = new MemoryStream())
                    {
                        screenshot = Camera.Capture(0, ScreenCaptureMode.Screen);
                        screenshot.Save(memory, ImageFormat.Png);
                        memory.Position = 0;

                        bitmapImage.BeginInit();
                        bitmapImage.StreamSource = memory;
                        bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                        bitmapImage.EndInit();
                    }

                    window.Background = new ImageBrush(bitmapImage);

                    window.Show();
                }));

            });

            task.ContinueWith(t =>
            {
                System.Drawing.Rectangle systemRect = new System.Drawing.Rectangle(topLeft.X, topLeft.Y, 
                    bottomRight.X-topLeft.X, bottomRight.Y-topLeft.Y);

                Bitmap tempBitmap = new Bitmap(systemRect.Width, systemRect.Height);

                using (Graphics g = Graphics.FromImage(tempBitmap))
                {
                    g.DrawImage(screenshot, new System.Drawing.Rectangle(0, 0, tempBitmap.Width, tempBitmap.Height),
                        systemRect, GraphicsUnit.Pixel);
                }

                Image = tempBitmap;

                TestItem testItem = testItemController.CurrentTestItem;
                OperationParameter operationParameter = testItem.Operation.GetParameterNamed("Image");
                operationParameter.Value = Image;

                Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    window.Close();
                    Thread.Sleep(500);
                    testItemController.RestoreTestItemEditorWindow();
                    Thread.Sleep(500);
                    testItemController.RestoreTestItemEditorWindow();
                }));

            });

            waitTask.Start();
            waitTask.ContinueWith(t => task.Start());

            window.Closed += (o, args) =>
            {
                doPicture = false;
                task.Dispose();
            };
        }

        private void ExecuteCancelParameterCommand()
        {
            testItemController.CloseEditParameterWindow();
        }

        private void ExecuteSaveParameterCommand()
        {
            testItemController.CloseEditParameterWindow();
        }
    }
}