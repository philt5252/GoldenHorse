using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Olf.GoldenHorse.Foundation.Services;

namespace Olf.GoldenHorse.Core.Services
{
    public class ExternalAppInfoManager : IExternalAppInfoManager
    {
        public string GetForegroundWindowProcessName()
        {
            IntPtr hwnd = GetForegroundWindow();
            uint pid;
            GetWindowThreadProcessId(hwnd, out pid);
            Process p = Process.GetProcessById((int)pid);
            return p.ProcessName;
        }

        public string GetForegroundWindowName()
        {
            StringBuilder builder = new StringBuilder(1024);

            IntPtr foregroundWindow = GetForegroundWindow();
            GetWindowText(foregroundWindow, builder, 1024);

            return builder.ToString();
        }

        public Point GetForegroundWindowLocalizedPoint()//for when window is already focused
        {
            RECT rect = GetWindowRect();
            int pX = Cursor.Position.X - rect.Left;
            int pY = Cursor.Position.Y - rect.Top;
            return new Point(pX, pY);
        }

        protected RECT GetWindowRect()
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
    }
}