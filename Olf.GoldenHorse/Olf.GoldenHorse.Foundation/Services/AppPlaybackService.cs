using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Automation;
using Castle.Core.Internal;
using Olf.Automation;
using Olf.GoldenHorse.Foundation.Models;
using TestStack.White;
using TestStack.White.Factory;
using TestStack.White.ScreenMap;
using TestStack.White.Sessions;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Actions;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.WindowItems;
using TestStack.White.UIItems.WPFUIItems;
using Point = System.Drawing.Point;

namespace Olf.GoldenHorse.Foundation.Services
{
    public static class AppPlaybackService
    {
        private const int SW_RESTORE = 9;

        public static void BringWindowToFront(int handle)
        {
            IntPtr handlePtr = new IntPtr(handle);

            if (GetForegroundWindow().ToInt32() == handle)
                return;

            ShowWindow(handlePtr, SW_RESTORE);
            SetForegroundWindow(handlePtr);

            Thread.Sleep(250);
        }

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern IntPtr WindowFromPoint(POINT Point);

        [DllImport("user32.dll")]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        private static List<string> GetWindowsForProcess(string process)
        {
            List<string> windows = new List<string>();
            StringBuilder builder = new StringBuilder(1024);

            foreach (IntPtr myIntPtr in EnumerateProcessWindowHandles(Process.GetProcessesByName(process).First().Id))
            {
                GetWindowText(myIntPtr, builder, 1024);

                string window = builder.ToString();
                windows.Add(window);
            }
            return windows;
        }

        delegate bool EnumThreadDelegate(IntPtr hWnd, IntPtr lParam);

        private static IEnumerable<IntPtr> EnumerateProcessWindowHandles(int processId)
        {
            var handles = new List<IntPtr>();

            foreach (ProcessThread thread in Process.GetProcessById(processId).Threads)
            {
                EnumThreadWindows(thread.Id,
                   (hWnd, lParam) => { handles.Add(hWnd); return true; }, IntPtr.Zero);
            }
               
            return handles;
        }

        [DllImport("user32.dll")]
        static extern bool EnumThreadWindows(int dwThreadId, EnumThreadDelegate lpfn,
            IntPtr lParam);


        public static IUIItem GetControl(AppProcess process, MappedItem window, MappedItem control, AppManager appManager)
        {
            Process wProcess = Process.GetProcessesByName(process.Name).First();
            Application application = Application.Attach(wProcess);

            Window appWindow = null;

            if (control.Type == "pane")
            {
                
            }

            /*IEnumerable<string> enumerable = application.GetWindows().Select(w => w.Title);
            IEnumerable<string> list2 = application.GetWindows().Select(w => w.AutomationElement.Current.Name);
*/
            List<string> windowTitles2 = GetWindowsForProcess(process.Name);

            if (!window.Name.IsNullOrEmpty())
                appWindow = application.GetWindow(SearchCriteria.ByAutomationId(window.Name), InitializeOption.NoCache);
            else
            {
                for (int i = 0; i < 5; i++)
                {
                    List<string> windowTitles = GetWindowsForProcess(process.Name);
                    string windowName = window.Text;
                    string closestTitle = windowTitles[0];
                    decimal toleranceLevel = windowName.Length * 0.7m;

                    foreach (string windowTitle in windowTitles)
                    {
                        int num1 = LevenshteinDistance.GetToleranceLevel(windowTitle, windowName);
                        if (num1 < toleranceLevel)
                        {
                            toleranceLevel = num1;
                            closestTitle = windowTitle;
                        }
                    }

                    try
                    {
                        List<Window> windows = application.GetWindows();

                        
                        appWindow = application.GetWindows().FirstOrDefault(w => w.AutomationElement.Current.Name.Equals(closestTitle));

                        if (appWindow == null)
                        {
                            List<IntPtr> handles = EnumerateProcessWindowHandles(wProcess.Id).ToList();

                            Dictionary<string, IntPtr> windowHandleDict = new Dictionary<string, IntPtr>();

                            StringBuilder builder = new StringBuilder(1024);

                            foreach (IntPtr myIntPtr in handles)
                            {
                                GetWindowText(myIntPtr, builder, 1024);

                                string windowTitle = builder.ToString();
                                windowHandleDict[windowTitle] = myIntPtr;
                            }

                            AutomationElement element = AutomationElement.FromHandle(windowHandleDict[closestTitle]);

                            appWindow = new Win32Window(element, WindowFactory.Desktop, InitializeOption.NoCache,
                                new NullWindowSession());
                        }

                        break;
                    }
                    catch (Exception ex)
                    {
                        
                    }
                    
                }
            }

            BringWindowToFront(appWindow.AutomationElement.Current.NativeWindowHandle);


            if (control == null || control.Type == "window" || control.Equals(window))
                return appWindow;

            Stack<MappedItem> mappedItemTree = new Stack<MappedItem>();
            MappedItem currentMappedItem = control;

            if (window == null)
            {
                
            }

            while (currentMappedItem != window)
            {
                mappedItemTree.Push(currentMappedItem);
                currentMappedItem = appManager.GetMappedItem(currentMappedItem.ParentId);
            }

            IUIItem currentControl = appWindow;
            AutomationElement elemn;

            while (mappedItemTree.Count > 0)
            {
                MappedItem currentMappedItemControl = mappedItemTree.Pop();

                if (!currentMappedItemControl.Name.IsNullOrEmpty())
                {
                    AutomationElement element = currentControl.AutomationElement.FindFirst(TreeScope.Descendants,
                        new PropertyCondition(AutomationElement.AutomationIdProperty, currentMappedItemControl.Name));
                    
                    //AutomationElement element = currentControl.GetElement(SearchCriteria.ByAutomationId(currentMappedItemControl.Name));
                    
                    currentControl = new UIItem(element, new NullActionListener());

                    continue;
                }

                if (!string.IsNullOrEmpty(currentMappedItemControl.Text))
                {
                    currentControl = new UIItem(currentControl.GetElement(SearchCriteria.ByText(currentMappedItemControl.Text)),
                        new NullActionListener());

                    continue;
                }
                
            }

            return currentControl;
        }

        private static AutomationElement GetChildControl(Rect bounds, AutomationElement automationElement)
        {
            TreeWalker walker = TreeWalker.ControlViewWalker;

            AutomationElement firstChild = walker.GetFirstChild(automationElement);

            if (firstChild == null)
                return null;

            while (firstChild != null)
            {
                if (Math.Abs(firstChild.Current.BoundingRectangle.X - bounds.X) < 0.1
                    && Math.Abs(firstChild.Current.BoundingRectangle.Y - bounds.Y) < 0.1)
                {
                    return GetChildControl(bounds, firstChild) ?? firstChild;
                }

                firstChild = walker.GetNextSibling(firstChild);
            }

            return null;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;

            public POINT(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }

            public POINT(Point pt)
                : this(pt.X, pt.Y)
            {
            }

            public static implicit operator Point(POINT p)
            {
                return new Point(p.X, p.Y);
            }

            public static implicit operator POINT(Point p)
            {
                return new POINT(p.X, p.Y);
            }
        }
    }
}