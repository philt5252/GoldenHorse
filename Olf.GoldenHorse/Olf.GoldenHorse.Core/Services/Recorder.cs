using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Forms;
using MouseKeyboardActivityMonitor;
using MouseKeyboardActivityMonitor.WinApi;
using Olf.GoldenHorse.Core.Models;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.Services;
using TestStack.White.UIItems;
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

        protected enum InputType
        {
            None,
            Mouse,
            Keyboard
        }

        private readonly Test test;
        private AppManager appManager { get { return test.Project.AppManager; } }
        private GlobalHooker globalHooker;
        private KeyboardHookListener keyboardHookListener;
        private MouseHookListener mouseHookListener;
        private RecorderState CurrentRecorderState;
        private InputType currentInputType;
        private IUIItem currentUiItem;
        private MappedItem currentMappedItem;

        public Recorder(Test test)
        {
            this.test = test;

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

            currentInputType = InputType.None;
        }

        private void MouseHookListenerOnMouseUp(object sender, MouseEventArgs mouseEventArgs)
        {
            if (currentInputType == InputType.Keyboard)
            {
                CreateKeyboardOnScreenAction();
            }

            currentInputType = InputType.Mouse;

            int processId = ExternalAppInfoManager.GetForegroundWindowProcessId();

            if (Process.GetCurrentProcess().Id == processId)
                return;

            IUIItem whiteControl;
            TestItem action = CreateOnScreenAction(mouseEventArgs, out whiteControl);
            currentUiItem = whiteControl;

            AutomationElement findWindowElement = FindWindowElement(whiteControl);

            int clickX = mouseEventArgs.Location.X - (int)whiteControl.Bounds.X;
            int clickY = mouseEventArgs.Location.Y - (int)whiteControl.Bounds.Y;
            ClickOperation clickOperation = CreateClickOperation(mouseEventArgs.Button, clickX, clickY);

            action.Operation = clickOperation;

            int screenshotX = mouseEventArgs.Location.X - (int)findWindowElement.Current.BoundingRectangle.X;
            int screenshotY = mouseEventArgs.Location.Y - (int)findWindowElement.Current.BoundingRectangle.Y;

            action.Screenshot.Adornments.Add(
                new ControlHighlightAdornment
                {
                    X = (int)whiteControl.Bounds.X - (int)findWindowElement.Current.BoundingRectangle.X,
                    Y = (int)whiteControl.Bounds.Y - (int)findWindowElement.Current.BoundingRectangle.Y,
                    Width=(int)whiteControl.Bounds.Width,
                    Height=(int)whiteControl.Bounds.Height
                });

            action.Screenshot.Adornments.Add(new ClickAdornment { ClickX = screenshotX, ClickY = screenshotY });
            
            test.TestItems.Add(action);
        }

        private static AutomationElement FindWindowElement(IUIItem whiteControl)
        {
            AutomationElement findWindowElement = whiteControl.AutomationElement;

            while (findWindowElement.Current.LocalizedControlType != "window")
            {
                findWindowElement = TreeWalker.ControlViewWalker.GetParent(findWindowElement);
            }
            return findWindowElement;
        }

        private void CreateKeyboardOnScreenAction()
        {
            TestItem onScreenAction = CreateOnScreenAction();
            onScreenAction.ControlId = currentMappedItem.Id;

            AutomationElement findWindowElement = FindWindowElement(currentUiItem);

            KeyboardOperation operation = new KeyboardOperation();
            operation.Text = keys;
            onScreenAction.Operation = operation;
            onScreenAction.Screenshot.Adornments.Add(
                new ControlHighlightAdornment
                {
                    X = (int)currentUiItem.Bounds.X - (int)findWindowElement.Current.BoundingRectangle.X,
                    Y = (int)currentUiItem.Bounds.Y - (int)findWindowElement.Current.BoundingRectangle.Y,
                    Width = (int)currentUiItem.Bounds.Width,
                    Height = (int)currentUiItem.Bounds.Height
                });

            test.TestItems.Add(onScreenAction);
        }

        private TestItem CreateOnScreenAction()
        {
            TestItem action = new TestItem{Type = TestItemTypes.OnScreenAction};
            Screenshot screenshot = CreateScreenshotFromCurrentBitmap();

            action.Screenshot = screenshot;

            return action;
        }

        private TestItem CreateOnScreenAction(MouseEventArgs mouseEventArgs, out IUIItem whiteControl)
        {
            TestItem action = new TestItem { Type = TestItemTypes.OnScreenAction };
            MappedItem mappedItem = null;
            Screenshot screenshot = CreateNewScreenshot();

            whiteControl = CreateWhiteControl(mouseEventArgs.Location, ref mappedItem);
            currentMappedItem = mappedItem;
            action.ControlId = mappedItem.Id;
            action.Screenshot = screenshot;
            return action;
        }

        private void TakePictureAndSetCurrentBitmap()
        {
            Task.Factory.StartNew(() =>
            {
                currentBitmap = Camera.Capture();
                currentBitmapDateTime = DateTime.Now;
            });

        }

        private Screenshot CreateNewScreenshot()
        {
            TakePictureAndSetCurrentBitmap();
            return CreateScreenshotFromCurrentBitmap();
        }

        private Screenshot CreateScreenshotFromCurrentBitmap()
        {
            Screenshot screenshot = new Screenshot();

            string screenshotName = "ghscn_" + currentBitmapDateTime.Ticks + ".bmp";
            
            screenshot.ImageFile = screenshotName;
            screenshot.DateTime = currentBitmapDateTime;

            currentBitmap.Save(Path.Combine(ProjectSuiteManager.GetScreenshotsFolder(test), screenshotName));

            return screenshot;
        }

        private ClickOperation CreateClickOperation(MouseButtons button, int clickX, int clickY)
        {
            ClickOperation clickOperation = null;

            if (button == MouseButtons.Left)
            {
                clickOperation = new LeftClickOperation();
            }
            else if (button == MouseButtons.Right)
            {
                clickOperation = new RightClickOperation();
            }

            clickOperation.SetClickPoint(clickX, clickY);
            return clickOperation;
        }

        private void MouseHookListenerOnMouseDown(object sender, MouseEventArgs mouseEventArgs)
        {
            //throw new NotImplementedException();
            TakePictureAndSetCurrentBitmap();
        }

        private void KeyboardHookListenerOnKeyUp(object sender, KeyEventArgs keyEventArgs)
        {
            TakePictureAndSetCurrentBitmap();
        }

        private int count = 0;
        private string keys = "";
        private Bitmap currentBitmap;
        private DateTime currentBitmapDateTime;

        private void KeyboardHookListenerOnKeyDown(object sender, KeyEventArgs keyEventArgs)
        {
            if (currentInputType == InputType.Mouse)
            {
                keys = "";
            }

            currentInputType = InputType.Keyboard;

            char keyValue = (char) keyEventArgs.KeyValue;

            if (!keyEventArgs.Shift)
                keyValue = char.ToLower(keyValue);

            keys += keyValue;
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
            keyboardHookListener.Enabled = true;
        }

        public void Stop()
        {
            CurrentRecorderState = RecorderState.Stopped;
            mouseHookListener.Enabled = false;
            keyboardHookListener.Enabled = false;
        }

        public void Pause()
        {
            CurrentRecorderState = RecorderState.Paused;
            mouseHookListener.Enabled = false;
            keyboardHookListener.Enabled = false;
        }

        public IUIItem CreateWhiteControl(Point point, ref MappedItem mappedItem)
        {

            //if (endurProcesses.Contains(windowState.WindowInfo.ProcessName))
            //return null;

            try
            {
                UIItem uiItem = ExternalAppInfoManager.GetControl(point);

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
                MappedItem createdMappedItem = null;

                while (uiElementTree.Count > 0)
                {
                    automationElement = uiElementTree.Pop();
                    string name = automationElement.Current.AutomationId;
                    string type = automationElement.Current.ControlType.LocalizedControlType;
                    string text = automationElement.Current.Name;
                    Rect bounds = automationElement.Current.BoundingRectangle;
                    bounds.X -= window.Current.BoundingRectangle.X;
                    bounds.Y -= window.Current.BoundingRectangle.Y;
                    createdMappedItem = appManager.FindOrCreateMappedItem(parentId, name, bounds, type, text);

                    parentId = createdMappedItem.Id;
                }

                mappedItem = createdMappedItem;

                return uiItem;
            }
            catch (Exception ex)
            {

                return null;
            }

        }

    }
}