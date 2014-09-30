using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Forms;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.Services;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Actions;
using Point = System.Drawing.Point;

namespace Olf.GoldenHorse.Core.Services
{
    public static class ExternalAppInfoManager
    {
        public static string GetForegroundWindowProcessName()
        {
            IntPtr hwnd = GetForegroundWindow();
            uint pid;
            GetWindowThreadProcessId(hwnd, out pid);
            Process p = Process.GetProcessById((int)pid);
            return p.ProcessName;
        }

        public static int GetForegroundWindowProcessId()
        {
            IntPtr hwnd = GetForegroundWindow();
            uint pid;
            GetWindowThreadProcessId(hwnd, out pid);
            return (int)pid;
        }

        public static string GetForegroundWindowName()
        {
            StringBuilder builder = new StringBuilder(1024);

            IntPtr foregroundWindow = GetForegroundWindow();
            GetWindowText(foregroundWindow, builder, 1024);

            return builder.ToString();
        }

        public static Point GetForegroundWindowLocalizedPoint()//for when window is already focused
        {
            RECT rect = GetWindowRect();
            int pX = Cursor.Position.X - rect.Left;
            int pY = Cursor.Position.Y - rect.Top;
            return new Point(pX, pY);
        }

        private static RECT GetWindowRect()
        {
            IntPtr foregroundWindow = GetForegroundWindow();
            RECT rect = new RECT();
            GetWindowRect(foregroundWindow, ref rect);
            return rect;
        }

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetWindowRect(IntPtr hWnd, ref RECT lpRect);

        [DllImport("user32.dll")]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);


        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowThreadProcessId(IntPtr hWnd, out uint ProcessId);

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

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        public static UIItem GetControl(Point point)//should I only make automationElement and actionListener once?
        {
            POINT point1 = new POINT(point.X, point.Y);
            IntPtr myIntPtr = WindowFromPoint(point1);

            AutomationElement automationElement = AutomationElement.FromHandle(myIntPtr);

            automationElement = GetChildControl(point, automationElement) ?? automationElement;

            TreeWalker walker = TreeWalker.ControlViewWalker;

            AutomationElement parentElement = walker.GetParent(automationElement);
            if (parentElement != null && parentElement.Current.LocalizedControlType == "button")
                automationElement = parentElement;

            int delNum;

            while(((string.IsNullOrEmpty(automationElement.Current.Name)
                        && string.IsNullOrEmpty(automationElement.Current.AutomationId))
                        || int.TryParse(automationElement.Current.AutomationId, out delNum)))
            {
                automationElement = walker.GetParent(automationElement);
            }


            ActionListener actionListener = new NullActionListener();
            return new UIItem(automationElement, actionListener);
        }

        private static AutomationElement GetChildControl(Point point, AutomationElement automationElement)
        {
            TreeWalker walker = TreeWalker.ControlViewWalker;

            AutomationElement firstChild = walker.GetFirstChild(automationElement);

            if (firstChild == null)
                return null;

            while (firstChild != null)
            {
                if (firstChild.Current.BoundingRectangle.Contains(new System.Windows.Point(point.X, point.Y)))
                {
                    return GetChildControl(point, firstChild) ?? firstChild;
                }

                firstChild = walker.GetNextSibling(firstChild);
            }

            return null;
        }

        [DllImport("user32.dll")]
        public static extern IntPtr WindowFromPoint(POINT Point);

        public static MappedItem GetMappedItemFromUIItem(IUIItem uiItem, AppManager appManager)
        {
            try
            {
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

                    return appManager.FindOrCreateMappedItem(appProcess1.Id, name, bounds, type, text);

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

                return createdMappedItem;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public static AutomationElement GetWindowAutomationElement(IUIItem uiItem)
        {
            AutomationElement findWindowElement = uiItem.AutomationElement;

            while (findWindowElement.Current.LocalizedControlType != "window" 
                && !(findWindowElement.Current.LocalizedControlType == "pane"
                    && ( TreeWalker.ControlViewWalker.GetParent(findWindowElement) == null || TreeWalker.ControlViewWalker.GetParent(findWindowElement).Current.LocalizedControlType == "process")))
            {
                findWindowElement = TreeWalker.ControlViewWalker.GetParent(findWindowElement);
            }

            return findWindowElement;
        }
    }
}