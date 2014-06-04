using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.Services;

namespace Olf.GoldenHorse.Core.Services
{
    public static class Camera
    {
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        private static extern IntPtr GetWindowRect(IntPtr hWnd, ref Rect rect);

        [StructLayout(LayoutKind.Sequential)]
        private struct Rect
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        public static Bitmap Capture(ScreenCaptureMode screenCaptureMode = ScreenCaptureMode.Window)
        {
            Rectangle bounds;
            var rect = new Rect();

            if (screenCaptureMode == ScreenCaptureMode.Screen)
            {
                bounds = Screen.GetBounds(Point.Empty);
            }
            else
            {

                IntPtr foregroundWindowsHandle = (IntPtr)0;
                DateTime dateTime = DateTime.Now;

                while (foregroundWindowsHandle == (IntPtr)0)
                {
                    if (DateTime.Now - dateTime > TimeSpan.FromSeconds(5))
                    {
                        throw new Exception("Couldnt get the foreground window...");
                    }

                    Thread.Sleep(100);
                    foregroundWindowsHandle = GetForegroundWindow();
                }


                GetWindowRect(foregroundWindowsHandle, ref rect);
                bounds = new Rectangle(rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom - rect.Top);
                //CursorPosition = new Point(Cursor.Position.X - rect.Left, Cursor.Position.Y - rect.Top);
            }

            var result = new Bitmap(bounds.Width, bounds.Height);

            using (var g = Graphics.FromImage(result))
            {
                g.CopyFromScreen(new Point(bounds.Left, bounds.Top), Point.Empty, bounds.Size);


                return result;
            }

        }
    }

    public enum ScreenCaptureMode
    {
        Window,
        Screen
    };

}