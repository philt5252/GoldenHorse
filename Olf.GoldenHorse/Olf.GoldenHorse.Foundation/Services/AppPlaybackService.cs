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
using Olf.GoldenHorse.Foundation.Models;
using TestStack.White;
using TestStack.White.Factory;
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

        public static IUIItem GetControl(AppProcess process, MappedItem window, MappedItem control, AppManager appManager)
        {
            Process wProcess = Process.GetProcessesByName(process.Name).First();
            Application application = Application.Attach(wProcess);

            Window appWindow;

            List<Window> list = application.GetWindows();
            string nam1e = list[1].Name;
            string title = list[1].Title;

            if (!window.Name.IsNullOrEmpty())
                appWindow = application.GetWindow(SearchCriteria.ByAutomationId(window.Name), InitializeOption.NoCache);
            else
            {
                List<Window> windows = application.GetWindows();
                string name = windows[0].AutomationElement.Current.Name;
                bool equals = Equals(name, window.Text);

                appWindow = windows.First(w => w.AutomationElement.Current.Name == window.Text);
            }

            BringWindowToFront(appWindow.AutomationElement.Current.NativeWindowHandle);


            if (control == null)
                return appWindow;

            Stack<MappedItem> mappedItemTree = new Stack<MappedItem>();
            MappedItem currentMappedItem = control;

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
                    AutomationElement element = currentControl.GetElement(SearchCriteria.ByAutomationId(currentMappedItemControl.Name));
                    
                    currentControl = new UIItem(element, new NullActionListener());

                    continue;
                }

                if (!string.IsNullOrEmpty(currentMappedItemControl.Text))
                {
                    currentControl = new UIItem(currentControl.GetElement(SearchCriteria.ByText(currentMappedItemControl.Text)),
                        new NullActionListener());

                    continue;
                }
                


                /*TreeWalker walker = TreeWalker.ControlViewWalker;

                AutomationElement automationElement = walker.GetFirstChild(currentControl.AutomationElement);

                Rect boundingRectangle = automationElement.Current.BoundingRectangle;

                AutomationElement childControl = GetChildControl(boundingRectangle, automationElement);*/
                //currentControl = new UIItem(childControl, new NullActionListener());

                /*while ((int)boundingRectangle.X != (int)currentMappedItemControl.Bounds.X)
                {
                    automationElement = walker.GetNextSibling(automationElement);
                    boundingRectangle = automationElement.Current.BoundingRectangle;
                }*/

                //.FirstOrDefault(i => Equals(i.AutomationElement.Current.AutomationId, control.Name));

                /*IUIItem[] uiItems = appWindow.Items
                    .Where(i => i.AutomationElement.Current.Name == control.Text
                    && i.AutomationElement.Current.LocalizedControlType == control.Type).ToArray();*/
            }

            //appWindow.Items.FirstOrDefault(i => Equals(i.AutomationElement.Current.AutomationId, mappedItemTree.Pop()));

            return currentControl;

            //if (uiItems.Length == 1)
            //    return uiItems[0];
                
           // return appWindow.Items.FirstOrDefault(i => Equals(control.Bounds, i.Bounds));
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