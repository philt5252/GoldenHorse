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

            /*IEnumerable<string> enumerable = application.GetWindows().Select(w => w.Title);
            IEnumerable<string> list2 = application.GetWindows().Select(w => w.AutomationElement.Current.Name);
*/
            if (!window.Name.IsNullOrEmpty())
                appWindow = application.GetWindow(SearchCriteria.ByAutomationId(window.Name), InitializeOption.NoCache);
            else
            {
                appWindow = application.GetWindowWhere(w => w.AutomationElement.Current.Name == window.Text, 45000);
                //appWindow = windows.First(w => w.AutomationElement.Current.Name == window.Text);
            }

            BringWindowToFront(appWindow.AutomationElement.Current.NativeWindowHandle);


            if (control == null || control.Type == "window")
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
                
                //appWindow.AutomationElement.FindFirst(TreeScope.Descendants,new PropertyCondition() )

                

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