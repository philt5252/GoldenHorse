using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Forms;
using MouseKeyboardActivityMonitor;
using MouseKeyboardActivityMonitor.WinApi;
using Olf.GoldenHorse.Core.Models;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.Services;
using TestStack.White.UIItems;
using ICamera = Olf.GoldenHorse.Foundation.Services.ICamera;
using KeyEventArgs = System.Windows.Forms.KeyEventArgs;
using MouseEventArgs = System.Windows.Forms.MouseEventArgs;
using Point = System.Drawing.Point;

namespace Olf.GoldenHorse.Core.Services
{
    public class Recorder : IRecorder
    {
        protected enum RecorderState
        {
            Recording,
            Stopped,
            Paused
        };

        private readonly Test test;
        private readonly IExternalAppInfoManager externalAppInfoManager;
        private readonly ICamera camera;
        private AppManager appManager { get { return test.Project.AppManager; } }
        private GlobalHooker globalHooker;
        private KeyboardHookListener keyboardHookListener;
        private MouseHookListener mouseHookListener;
        private RecorderState CurrentRecorderState;

        public Recorder(Test test, IExternalAppInfoManager externalAppInfoManager,
            ICamera camera)
        {
            this.test = test;
            this.externalAppInfoManager = externalAppInfoManager;
            this.camera = camera;

            CurrentRecorderState = RecorderState.Stopped;

            globalHooker = new GlobalHooker();

            keyboardHookListener = new KeyboardHookListener(globalHooker);
            mouseHookListener = new MouseHookListener(globalHooker);

            keyboardHookListener.KeyDown += KeyboardHookListenerOnKeyDown;
            keyboardHookListener.KeyUp += KeyboardHookListenerOnKeyUp;

            mouseHookListener.MouseDown += MouseHookListenerOnMouseDown;
            mouseHookListener.MouseUp += MouseHookListenerOnMouseUp;

            mouseHookListener.Enabled = false;
            mouseHookListener.Enabled = false;
        }

        private void MouseHookListenerOnMouseUp(object sender, MouseEventArgs mouseEventArgs)
        {
            OnScreenAction action = new OnScreenAction();

            string processName = externalAppInfoManager.GetForegroundWindowProcessName();
            string foregroundWindowName = externalAppInfoManager.GetForegroundWindowName();

            if (processName.Contains("Olf.GoldenHorse"))
                return;

            IUIItem whiteControl = CreateWhiteControl(mouseEventArgs.Location);

            action.WindowName = foregroundWindowName;
            action.ControlName = foregroundWindowName;

            

            ClickOperation clickOperation = CreateClickOperation(mouseEventArgs);

            action.Operation = clickOperation;

            Screenshot screenshot = GetScreenshot(action);

            Point clickPoint = clickOperation.GetClickPoint();
            screenshot.Adornments.Add(new ScreenshotClickAdornment { ClickX = clickPoint.X, ClickY = clickPoint.Y });
            action.Screenshot = screenshot;
            test.TestItems.Add(action);
        }

        private Screenshot GetScreenshot(OnScreenAction action)
        {
            Screenshot screenshot = new Screenshot();
            Bitmap bitmap = camera.Capture();
            DateTime dateTime = DateTime.Now;
            string screenshotName = "ghscn_" + dateTime.Ticks + ".bmp";
            bitmap.Save(Path.Combine(ProjectSuiteManager.GetScreenshotsFolder(test), screenshotName));

            screenshot.ImageFile = screenshotName;
            screenshot.DateTime = dateTime;
            screenshot.Owner = action;
            return screenshot;
        }

        private ClickOperation CreateClickOperation(MouseEventArgs mouseEventArgs)
        {
            ClickOperation clickOperation = null;

            if (mouseEventArgs.Button == MouseButtons.Left)
            {
                clickOperation = new LeftClickOperation();
            }
            else if (mouseEventArgs.Button == MouseButtons.Right)
            {
                clickOperation = new RightClickOperation();
            }

            Point localizedPoint = externalAppInfoManager.GetForegroundWindowLocalizedPoint();
            clickOperation.SetClickPoint(localizedPoint.X, localizedPoint.Y);
            return clickOperation;
        }

        private void MouseHookListenerOnMouseDown(object sender, MouseEventArgs mouseEventArgs)
        {
            //throw new NotImplementedException();
        }

        private void KeyboardHookListenerOnKeyUp(object sender, KeyEventArgs keyEventArgs)
        {
            //throw new NotImplementedException();
        }

        private void KeyboardHookListenerOnKeyDown(object sender, KeyEventArgs keyEventArgs)
        {
            //throw new NotImplementedException();
        }

        public Test CurrentTest
        {
            get { return test; }
        }

        public void Record()
        {
            CurrentRecorderState = RecorderState.Recording;
            mouseHookListener.Enabled = true;
            mouseHookListener.Enabled = true;
        }

        public void Stop()
        {
            CurrentRecorderState = RecorderState.Stopped;
            mouseHookListener.Enabled = false;
            mouseHookListener.Enabled = false;
        }

        public void Pause()
        {
            CurrentRecorderState = RecorderState.Paused;
            mouseHookListener.Enabled = false;
            mouseHookListener.Enabled = false;
        }

        public IUIItem CreateWhiteControl(Point point)
        {

            //if (endurProcesses.Contains(windowState.WindowInfo.ProcessName))
            //return null;

            try
            {
                UIItem uiItem = externalAppInfoManager.GetControl(point);

                if (uiItem.AutomationElement.Current.LocalizedControlType.Equals("window"))
                    return null;

                TreeWalker walker = TreeWalker.ControlViewWalker;
                AutomationElement automationElement = uiItem.AutomationElement;
                Stack<AutomationElement> uiElementTree = new Stack<AutomationElement>();

                while (automationElement != AutomationElement.RootElement)
                {
                    uiElementTree.Push(automationElement);
                    automationElement = walker.GetParent(automationElement);
                }

                Process process = Process.GetProcessById(uiItem.AutomationElement.Current.ProcessId);

                AppProcess appProcess = appManager.FindOrCreateProcess(process.ProcessName);
                string parentId = appProcess.Id;

                AutomationElement window = uiElementTree.Peek();

                while (uiElementTree.Count > 0)
                {
                    automationElement = uiElementTree.Pop();
                    string name = automationElement.Current.AutomationId;
                    string type = automationElement.Current.ControlType.LocalizedControlType;
                    Rect bounds = automationElement.Current.BoundingRectangle;
                    bounds.X -= window.Current.BoundingRectangle.X;
                    bounds.Y -= window.Current.BoundingRectangle.Y;
                    MappedItem mappedItem = appManager.FindOrCreateMappedItem(parentId, name, bounds, type);

                    parentId = mappedItem.Id;
                }

                //Get rid of form or window
                //uiElementTree.Pop();

                
                while (uiElementTree.Count > 0)
                {
                    //string pop = uiElementTree.Pop();
                }


                /*IWhiteControl control = whiteControlFactory.Create();
                control.ClassName = uiItem.AutomationElement.Current.ClassName;
                control.Window = windowState.WindowInfo.WindowName;
                control.Name = uiItem.Id;
                control.Process = windowState.WindowInfo.ProcessName;

                System.Windows.Point localizedUiItemLocation = new System.Windows.Point(
                    uiItem.Bounds.X - windowLocation.X,
                    uiItem.Bounds.Y - windowLocation.Y);

                control.Bounds = new Rect(localizedUiItemLocation, uiItem.Bounds.Size);



                control.ParentTree = uiElementTree.ToArray();
                ControlManager.AddControlToDictionary(control, windowState);*/


                //return control;
                return uiItem;
            }
            catch (Exception ex)
            {

                return null;
            }

        }

    }
}