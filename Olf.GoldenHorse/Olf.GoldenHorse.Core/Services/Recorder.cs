using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Forms;
using KeyboardFormatterApp;
using Microsoft.Practices.Prism;
using MouseKeyboardActivityMonitor;
using MouseKeyboardActivityMonitor.WinApi;
using Olf.GoldenHorse.Core.Models;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.Services;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Actions;
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
        private GlobalHooker globalHooker2;
        private KeyboardHookListener keyboardHookListener;
        private MouseHookListener mouseHookListener;
        private RecorderState CurrentRecorderState;
        private InputType currentInputType;
        private IUIItem currentUiItem;
        private MappedItem currentMappedItem;
        private KeyboardHelper keyboardHelper;
        private HashSet<Task> executingTasks = new HashSet<Task>();
        private List<TestItem> newTestItems = new List<TestItem>(); 

        public int InsertPosition { get; set; }

        public string CurrentText { get { return keyboardHelper.KeyboardText; } }

        public bool ScreenshotsEnabled { get; set; }

        public TestItem[] NewTestItems{ get { return newTestItems.ToArray(); } }

        public Recorder(Test test)
        {
            this.test = test;

            ScreenshotsEnabled = true;
            CurrentRecorderState = RecorderState.Stopped;

            globalHooker = new GlobalHooker();
            globalHooker2 = new GlobalHooker();

            keyboardHookListener = new KeyboardHookListener(globalHooker2);
            mouseHookListener = new MouseHookListener(globalHooker);

            keyboardHookListener.KeyDown += KeyboardHookListenerOnKeyDown;
            keyboardHookListener.KeyUp += KeyboardHookListenerOnKeyUp;

            mouseHookListener.MouseDown += MouseHookListenerOnMouseDown;
            mouseHookListener.MouseUp += MouseHookListenerOnMouseUp;

            mouseHookListener.Enabled = false;
            mouseHookListener.Enabled = false;

            keyboardHelper = new KeyboardHelper();

            currentInputType = InputType.None;

            InsertPosition = -1;
        }

        private void MouseHookListenerOnMouseUp(object sender, MouseEventArgs mouseEventArgs)
        {
            Task task = new Task(() =>
            {
                Thread.Sleep(100);
                if (currentInputType == InputType.Keyboard)
                {
                    if (keyboardHelper.KeyboardText != "")
                    {
                        CreateKeyboardOnScreenAction(keyboardHelper.KeyboardText);
                        keyboardHelper.ResetKeyboardText();
                    }
                 
                }

                currentInputType = InputType.Mouse;

                int processId = ExternalAppInfoManager.GetForegroundWindowProcessId();

                if (Process.GetCurrentProcess().Id == processId)
                    return;

                IUIItem whiteControl;
                TestItem action = CreateOnScreenAction(mouseEventArgs, out whiteControl);
                currentUiItem = whiteControl;

                AutomationElement findWindowElement = FindWindowElement(whiteControl);

                int clickX = mouseEventArgs.Location.X - (int) whiteControl.Bounds.X;
                int clickY = mouseEventArgs.Location.Y - (int) whiteControl.Bounds.Y;
                ClickOperation clickOperation = CreateClickOperation(mouseEventArgs.Button, clickX, clickY);

                action.Operation = clickOperation;

                if (ScreenshotsEnabled)
                {
                    int screenshotX = mouseEventArgs.Location.X - (int) findWindowElement.Current.BoundingRectangle.X;
                    int screenshotY = mouseEventArgs.Location.Y - (int) findWindowElement.Current.BoundingRectangle.Y;

                    action.Screenshot.Adornments.Add(
                        new ControlHighlightAdornment
                        {
                            X = (int) whiteControl.Bounds.X - (int) findWindowElement.Current.BoundingRectangle.X,
                            Y = (int) whiteControl.Bounds.Y - (int) findWindowElement.Current.BoundingRectangle.Y,
                            Width = (int) whiteControl.Bounds.Width,
                            Height = (int) whiteControl.Bounds.Height
                        });

                    action.Screenshot.Adornments.Add(new ClickAdornment {ClickX = screenshotX, ClickY = screenshotY});
                }
                action.Test = test;
                newTestItems.Add(action);
                //test.TestItems.Add(action);
            });

            AddTaskToExecutingListAndStart(task);
        }

        private void AddTaskToExecutingListAndStart(Task task)
        {
            executingTasks.Add(task);
            task.ContinueWith(t => executingTasks.Remove(task));
            task.RunSynchronously();
            task.Start();
            
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

        private void CreateKeyboardOnScreenAction(String keys)
        {
            TestItem onScreenAction = CreateOnScreenAction();
            onScreenAction.ControlId = currentMappedItem.Id;

            AutomationElement findWindowElement = FindWindowElement(currentUiItem);

            KeyboardOperation operation = new KeyboardOperation();
            operation.Text = keys;
            onScreenAction.Operation = operation;

            if (ScreenshotsEnabled)
            {
                onScreenAction.Screenshot.Adornments.Add(
                    new ControlHighlightAdornment
                    {
                        X = (int) currentUiItem.Bounds.X - (int) findWindowElement.Current.BoundingRectangle.X,
                        Y = (int) currentUiItem.Bounds.Y - (int) findWindowElement.Current.BoundingRectangle.Y,
                        Width = (int) currentUiItem.Bounds.Width,
                        Height = (int) currentUiItem.Bounds.Height
                    });
            }

            newTestItems.Add(onScreenAction);
            //test.TestItems.Add(onScreenAction);
        }

        private TestItem CreateOnScreenAction()
        {
            TestItem action = new TestItem{Type = TestItemTypes.OnScreenAction};

            if (ScreenshotsEnabled)
            {
                Screenshot screenshot = CreateScreenshotFromCurrentBitmap();

                action.Screenshot = screenshot;
            }

            return action;
        }

        private TestItem CreateOnScreenAction(MouseEventArgs mouseEventArgs, out IUIItem whiteControl)
        {
            TestItem action = new TestItem { Type = TestItemTypes.OnScreenAction };
            MappedItem mappedItem = null;
            

            whiteControl = CreateWhiteControl(mouseEventArgs.Location, ref mappedItem);
            currentMappedItem = mappedItem;
            action.ControlId = mappedItem.Id;

            if (ScreenshotsEnabled)
            {
                Screenshot screenshot = CreateNewScreenshot();
                action.Screenshot = screenshot;
            }
            
            return action;
        }

        private void TakePictureAndSetCurrentBitmap()
        {
            currentBitmap = Camera.Capture();
            currentBitmapDateTime = DateTime.Now;
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
            mouseDownMouseEventArgs = mouseEventArgs;
            //throw new NotImplementedException();
            TakePictureAndSetCurrentBitmap();
        }

        private void KeyboardHookListenerOnKeyUp(object sender, KeyEventArgs e)
        {
            keyboardHelper.ManageKeysOnUp(e);

            if(ScreenshotsEnabled)
                TakePictureAndSetCurrentBitmap();
        }

        private int count = 0;
        private Bitmap currentBitmap;
        private DateTime currentBitmapDateTime;

        private void KeyboardHookListenerOnKeyDown(object sender, KeyEventArgs e)
        {
            Task task = new Task(() =>
            {
                if (currentInputType == InputType.Mouse)
                {
                    keyboardHelper.KeyboardText = "";
                }

                currentInputType = InputType.Keyboard;

                if (keyboardHelper.IsFunctionKey(e))
                {
                    keyboardHelper.CreateKeyboardFunctionText(e);
                    if (keyboardHelper.KeyboardText != null && keyboardHelper.KeyboardText != "")
                    {
                        CreateKeyboardOnScreenAction(keyboardHelper.KeyboardText);
                        keyboardHelper.ResetKeyboardText();
                    }
                    CreateKeyboardOnScreenAction(keyboardHelper.KeyboardFunctionText);
                    keyboardHelper.KeyboardFunctionText = "";
                }
                
                else
                {
               
                    if (keyboardHelper.IsCtrlPressed)
                    {
                        keyboardHelper.ManageKeysOnDown(e);
                        if (keyboardHelper.KeyboardText != null && keyboardHelper.KeyboardText != "")
                        {
                            CreateKeyboardOnScreenAction(keyboardHelper.KeyboardText);
                            keyboardHelper.ResetKeyboardText();
                        }
                        CreateKeyboardOnScreenAction(keyboardHelper.KeyboardFunctionText);
                        keyboardHelper.KeyboardFunctionText = "";
                    }
                    else
                    {
                        keyboardHelper.ManageKeysOnDown(e);
                    }
                }

                
                //throw new NotImplementedException();
            });

            AddTaskToExecutingListAndStart(task);
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
            if (CurrentRecorderState == RecorderState.Stopped)
            {
                return;
            }

            CurrentRecorderState = RecorderState.Stopped;

            mouseHookListener.Enabled = false;
            keyboardHookListener.Enabled = false;

            while (executingTasks.Count > 0)
            {
                try
                {
                    Task task = executingTasks.FirstOrDefault();

                    if (task == null)
                        continue;

                    if (task.IsCompleted)
                        executingTasks.Remove(task);

                    task.Wait();
                }
                catch (Exception e)
                {
                    
                }
                
            }

            if (InsertPosition < 0)
            {
                test.TestItems.AddRange(newTestItems);
                return;
            }

            for (int i = 0; i < newTestItems.Count; i++)
            {
                test.TestItems.Insert(InsertPosition + i, newTestItems[0]);
            }
            
        }

        public void Pause()
        {
            CurrentRecorderState = RecorderState.Paused;
            mouseHookListener.Enabled = false;
            keyboardHookListener.Enabled = false;
        }

        private string[] endurProcesses = new string[] { "master", "master.exe", "olfpres", "olfpres.exe", "ins", "ins.exe" };
        private MouseEventArgs mouseDownMouseEventArgs;

        public IUIItem CreateWhiteControl(Point point, ref MappedItem mappedItem)
        {
            try
            {
                UIItem uiItem = ExternalAppInfoManager.GetControl(point);

                if (uiItem.AutomationElement.Current.LocalizedControlType.Equals("window"))
                {
                    Process process1 = Process.GetProcessById(uiItem.AutomationElement.Current.ProcessId);

                    AppProcess appProcess1 = appManager.FindOrCreateProcess(process1.ProcessName);

                    
                    string name = uiItem.AutomationElement.Current.AutomationId;

                    int num;
                    if (int.TryParse(name, out num))
                        name = "";

                    string type = uiItem.AutomationElement.Current.ControlType.LocalizedControlType;
                    string text = uiItem.AutomationElement.Current.Name;

                    if (type == "edit")
                        text = "";

                    Rect bounds = uiItem.AutomationElement.Current.BoundingRectangle;
                    bounds.X = 0;//;window.Current.BoundingRectangle.X;
                    bounds.Y = 0;//window.Current.BoundingRectangle.Y;
                    mappedItem = appManager.FindOrCreateMappedItem(appProcess1.Id, name, bounds, type, text);
                    
                    return uiItem;
                }
                    

                TreeWalker walker = TreeWalker.ControlViewWalker;
                AutomationElement automationElement = uiItem.AutomationElement;
                Stack<AutomationElement> uiElementTree = new Stack<AutomationElement>();

                int delNum;

                while (automationElement != AutomationElement.RootElement)
                {
                    if (!((string.IsNullOrEmpty(automationElement.Current.Name)
                        && string.IsNullOrEmpty(automationElement.Current.AutomationId))
                        || int.TryParse(automationElement.Current.AutomationId, out delNum)))
                    {
                        uiElementTree.Push(automationElement);
                    }
                    
                    automationElement = walker.GetParent(automationElement);
                }

                Process process = Process.GetProcessById(uiItem.AutomationElement.Current.ProcessId);

                AppProcess appProcess = appManager.FindOrCreateProcess(process.ProcessName);
                
                string parentId = appProcess.Id;

                AutomationElement window = uiElementTree.Peek();
                MappedItem createdMappedItem = null;

                /*if (endurProcesses.Contains(appProcess.Name))
                {
                    automationElement = uiElementTree.Pop();
                    string name = automationElement.Current.AutomationId;
                    string type = automationElement.Current.ControlType.LocalizedControlType;
                    string text = automationElement.Current.Name;
                    Rect bounds = automationElement.Current.BoundingRectangle;
                    bounds.X -= window.Current.BoundingRectangle.X;
                    bounds.Y -= window.Current.BoundingRectangle.Y;
                    createdMappedItem = appManager.FindOrCreateMappedItem(parentId, name, bounds, type, text);

                    mappedItem = createdMappedItem;

                    return new UIItem(window, new NullActionListener());
                }*/

                while (uiElementTree.Count > 0)
                {
                    automationElement = uiElementTree.Pop();
                    string name = automationElement.Current.AutomationId;

                    int num;
                    if (int.TryParse(name, out num))
                        name = "";

                    string type = automationElement.Current.ControlType.LocalizedControlType;
                    string text = automationElement.Current.Name;

                    if (type == "edit")
                        text = "";

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