using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Castle.Core.Internal;
using Olf.GoldenHorse.Foundation.Models;
using TestStack.White;
using TestStack.White.UIItems;
using TestStack.White.UIItems.WindowItems;

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

        public static IUIItem GetControl(AppProcess process, MappedItem window, MappedItem control)
        {
            Process wProcess = Process.GetProcessesByName(process.Name).First();
            Application application = Application.Attach(wProcess);

            Window appWindow;

            if (!window.Name.IsNullOrEmpty())
                appWindow = application.GetWindow(window.Name);
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

            if (!control.Name.IsNullOrEmpty())
                return appWindow.Items.FirstOrDefault(i => Equals(i.Name, control.Name));
            
            IUIItem[] uiItems = appWindow.Items
                .Where(i => i.AutomationElement.Current.Name == control.Text
                && i.AutomationElement.Current.LocalizedControlType == control.Type).ToArray();

            if (uiItems.Length == 1)
                return uiItems[0];
                
            return appWindow.Items.FirstOrDefault(i => Equals(control.Bounds, i.Bounds));
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