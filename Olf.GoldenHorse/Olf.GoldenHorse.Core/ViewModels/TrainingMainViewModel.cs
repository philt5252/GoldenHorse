
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.RightsManagement;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Microsoft.Practices.Prism.Commands;
using MouseKeyboardActivityMonitor;
using MouseKeyboardActivityMonitor.WinApi;
using Olf.GoldenHorse.Core.Models;
using Olf.GoldenHorse.Core.Services;
using Olf.GoldenHorse.Foundation.Controllers;
using Olf.GoldenHorse.Foundation.Factories.ViewModels;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.Services;
using Olf.GoldenHorse.Foundation.ViewModels;
using TestStack.White.UIItems;
using Application = System.Windows.Application;
using Cursor = System.Windows.Input.Cursor;

namespace Olf.GoldenHorse.Core.ViewModels
{
    public class TrainingMainViewModel : ViewModelBase, ITrainingMainViewModel
    {
        private readonly Test test;
        private readonly ITrainingItemViewModelFactory trainingItemViewModelFactory;
        private readonly ITrainingController trainingController;
        private ITrainingItemViewModel selectedTrainingItem;
        private Recorder recorder;
        private bool isActive = false;

        public List<ITrainingItemViewModel> TrainingItems { get; set; }

        public ITrainingItemViewModel SelectedTrainingItem
        {
            get { return selectedTrainingItem; }
            set
            {
                selectedTrainingItem = value;

                OnPropertyChanged("SelectedTrainingItem");
            }
        }

        public ICommand BeginCommand { get; protected set; }
        public ICommand StopCommand { get; protected set; }

        public TrainingMainViewModel(Test test, ITrainingItemViewModelFactory trainingItemViewModelFactory,
            ITrainingController trainingController)
        {
            this.test = test;
            this.trainingItemViewModelFactory = trainingItemViewModelFactory;
            this.trainingController = trainingController;

            TrainingItems = test.TestItems.Select(t => trainingItemViewModelFactory.Create(t)).ToList();

            SelectedTrainingItem = TrainingItems.First();

            BeginCommand = new DelegateCommand(ExecuteBeginCommand);
            StopCommand = new DelegateCommand(ExecuteStopCommand);
            
            
        }

        private void ExecuteStopCommand()
        {
            isActive = false;
            recorder.Stop();
            trainingController.Stop();
        }

        private void ExecuteBeginCommand()
        {
            recorder = new Recorder(new Test { Project = test.Project });
            recorder.ScreenshotsEnabled = false;
            isActive = true;

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
            Ellipse ellipse = new Ellipse();
            ellipse.StrokeThickness = 3;
            ellipse.Stroke = Brushes.Red;
            ellipse.Width = 0;
            ellipse.Height = 0;
            Canvas.SetTop(rectangle, 0);
            Canvas.SetLeft(rectangle, 0);

            Canvas.SetTop(ellipse, 0);
            Canvas.SetLeft(ellipse, 0);
            UIItem currentUIItem = null;
            System.Drawing.Point point = new System.Drawing.Point();

            Task task = new Task(() =>
            {
                recorder.Record();

                try
                {
                    while (SelectedTrainingItem != null && isActive)
                    {
                        TestItem testItem = recorder.NewTestItems.LastOrDefault();

                        if (testItem != null
                            && testItem.Control.Equals(SelectedTrainingItem.TestItem.Control)
                            && SelectedTrainingItem.TestItem.Operation.GetType() == testItem.Operation.GetType()
                            && SelectedTrainingItem.TestItem.Operation is ClickOperation)
                        {
                            AdvanceSelectedTrainingItem();
                        }
                        else if (SelectedTrainingItem.TestItem.Operation is KeyboardOperation
                                 && (SelectedTrainingItem.TestItem.Operation as KeyboardOperation).Text == recorder.CurrentText)
                        {
                            AdvanceSelectedTrainingItem();
                        }

                        point = System.Windows.Forms.Cursor.Position;

                        AppProcess process = test.Project.AppManager.GetProcess(SelectedTrainingItem.TestItem.Control);
                        MappedItem windowItem = test.Project.AppManager.GetWindow(SelectedTrainingItem.TestItem.Control);

                        IUIItem uiItem = AppPlaybackService.GetControl(process, windowItem,
                            SelectedTrainingItem.TestItem.Control, test.Project.AppManager);
                        //Point clickPoint = this.GetClickPoint();



                        Rect bounds = uiItem.AutomationElement.Current.BoundingRectangle;

                        //Thread.Sleep(500);
                        Application.Current.Dispatcher.Invoke(new Action(() =>
                        {
                            //rectangle.Width = 10;
                            //rectangle.Height = 10;
                            //Canvas.SetTop(rectangle, globalPoint.X - rectangle.Width);
                            //Canvas.SetLeft(rectangle, globalPoint.Y - rectangle.Height);
                            rectangle.Width = bounds.Width;
                            rectangle.Height = bounds.Height;
                            Canvas.SetTop(rectangle, bounds.Y);
                            Canvas.SetLeft(rectangle, bounds.X);
                        }));
                    }
                }
                catch (Exception ex)
                {

                }

                recorder.Stop();
            });

            canvas.Children.Add(rectangle);
            canvas.Children.Add(ellipse);
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
                Application.Current.Dispatcher.Invoke(new Action(window.Close));
            });

            window.Closed += (o, args) =>
            {
                isActive = false;
                task.Dispose();
            };
        }

        private void AdvanceSelectedTrainingItem()
        {
            SelectedTrainingItem.Status = TrainingItemStatus.Completed;

            int index = TrainingItems.IndexOf(SelectedTrainingItem);

            if (index < TrainingItems.Count - 1)
            {
                SelectedTrainingItem = TrainingItems[index + 1];
            }
            else
            {
                isActive = false;
            }
        }
    }
}