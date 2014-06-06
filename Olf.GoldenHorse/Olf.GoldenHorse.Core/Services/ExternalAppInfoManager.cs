﻿using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Automation;
using System.Windows.Forms;
using Olf.GoldenHorse.Foundation.Services;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Actions;

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
    }
}