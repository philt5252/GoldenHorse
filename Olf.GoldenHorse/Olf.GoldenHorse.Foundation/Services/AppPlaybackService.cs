﻿using System.Diagnostics;
using System.Linq;
using TestStack.White;
using TestStack.White.UIItems.WindowItems;

namespace Olf.GoldenHorse.Foundation.Services
{
    public static class AppPlaybackService
    {
        public static void GetControl(string processName, string windowName, string controlName)
        {
            Process process = Process.GetProcessesByName(processName).First();
            Application application = Application.Attach(process);

            Window window = application.GetWindow(windowName);

            
        }
    }
}