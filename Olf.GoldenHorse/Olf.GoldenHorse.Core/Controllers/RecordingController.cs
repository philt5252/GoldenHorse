using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using MouseKeyboardActivityMonitor;
using MouseKeyboardActivityMonitor.WinApi;
using Olf.GoldenHorse.Core.Models;
using Olf.GoldenHorse.Core.Services;
using Olf.GoldenHorse.Foundation.Controllers;
using Olf.GoldenHorse.Foundation.DataAccess;
using Olf.GoldenHorse.Foundation.Factories.Services;
using Olf.GoldenHorse.Foundation.Factories.ViewModels;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.Services;
using Olf.GoldenHorse.Foundation.ViewModels;
using Olf.GoldenHorse.Foundation.Views;
using Olf.GoldenHorse.Foundation.Views.Factories;
using TestStack.White.UIItems;
using Application = System.Windows.Application;

namespace Olf.GoldenHorse.Core.Controllers
{
    public class RecordingController : IRecordingController
    {
        private readonly IRecordWindowFactory recordWindowFactory;
        private readonly IRecorderViewModelFactory recorderViewModelFactory;
        private readonly IRecorderFactory recorderFactory;
        private readonly ITestFileManager testFileManager;
        private readonly IProjectFileManager projectFileManager;
        private readonly IAppController appController;
        private readonly ITestController testController;
        private readonly ITestItemEditorViewModelFactory testItemEditorViewModelFactory;
        private readonly ITestItemEditorWindowFactory testItemEditorWindowFactory;
        private IWindow recordingWindow;
        private IRecorder recorder;

        public RecordingController(IRecordWindowFactory recordWindowFactory,
            IRecorderViewModelFactory recorderViewModelFactory,
            IRecorderFactory recorderFactory,
            ITestFileManager testFileManager,
            IProjectFileManager projectFileManager, 
            IAppController appController,
            ITestController testController,
            ITestItemEditorViewModelFactory testItemEditorViewModelFactory,
            ITestItemEditorWindowFactory testItemEditorWindowFactory)
        {
            this.recordWindowFactory = recordWindowFactory;
            this.recorderViewModelFactory = recorderViewModelFactory;
            this.recorderFactory = recorderFactory;
            this.testFileManager = testFileManager;
            this.projectFileManager = projectFileManager;
            this.appController = appController;
            this.testController = testController;
            this.testItemEditorViewModelFactory = testItemEditorViewModelFactory;
            this.testItemEditorWindowFactory = testItemEditorWindowFactory;
        }

        public void ShowRecord()
        {
            Project defaultProject = ProjectSuiteManager.GetDefaultProject();

            Test test = testFileManager.CreateTestForProject(defaultProject);

            recordingWindow = recordWindowFactory.Create();

            recorder = recorderFactory.Create(test);
            IRecorderViewModel recorderViewModel = recorderViewModelFactory.Create(recorder);

            recordingWindow.DataContext = recorderViewModel;

            //appController.MainWindow.Minimize();

            recordingWindow.Show();
            appController.MainWindow.Minimize();
        }

        public void StopRecord()
        {
            recordingWindow.Close();

            testFileManager.Save(recorder.CurrentTest);
            projectFileManager.Save(recorder.CurrentTest.Project);

            appController.MainWindow.Restore();
            testController.ShowTest(recorder.CurrentTest);
        }

        public void DoValidation(OnScreenValidation onScreenValidation)
        {
            IWindow testItemEditorWindow = testItemEditorWindowFactory.Create();
            ITestItemEditorViewModel testItemEditorViewModel = testItemEditorViewModelFactory.Create();

            testItemEditorWindow.DataContext = testItemEditorViewModel;

            testItemEditorWindow.ShowDialog();

            return;

            Window window = new Window();
            window.Height = SystemParameters.VirtualScreenHeight;
            window.Width = SystemParameters.VirtualScreenWidth;
            bool doPicture = false;

            Canvas canvas = new Canvas();
            Rectangle rectangle = new Rectangle();
            rectangle.StrokeThickness = 2;
            rectangle.Stroke = Brushes.CornflowerBlue;
            rectangle.Height = 0;
            rectangle.Width = 0;
            Canvas.SetTop(rectangle, 0);
            Canvas.SetLeft(rectangle, 0);

            Task task = new Task(() =>
            {
                doPicture = true;
                UIItem prevUIItem = null;
                UIItem currentUIITem = null;

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
                    currentUIITem = ExternalAppInfoManager.GetControl(System.Windows.Forms.Cursor.Position);

                    if (currentUIITem.AutomationElement.Current.ProcessId == Process.GetCurrentProcess().Id)
                        currentUIITem = prevUIItem;

                    if (currentUIITem == null)
                        continue;

                    Rect bounds = currentUIITem.AutomationElement.Current.BoundingRectangle;


                    Thread.Sleep(250);
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        rectangle.Width = bounds.Width;
                        rectangle.Height = bounds.Height;
                        Canvas.SetTop(rectangle, bounds.Y);
                        Canvas.SetLeft(rectangle, bounds.X);
                    }));
                    prevUIItem = currentUIITem;
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
                Application.Current.Dispatcher.Invoke(new Action(() => window.Close()));
            });

            window.Closed += (o, args) =>
            {
                doPicture = false;
                task.Dispose();
            };
        }
    }
}