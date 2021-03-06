﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using MahApps.Metro.Controls;
using Olf.GoldenHorse.Foundation.Views;

namespace Olf.GoldenHorse.Core.Views
{
    //BaseLight
    public class WindowBase : MetroWindow, IWindow
    {
        public void Maximize()
        {
            this.WindowState = WindowState.Maximized;
        }

        public void Minimize()
        {
            this.WindowState = WindowState.Minimized;
        }

        public void Restore()
        {
            this.WindowState = WindowState.Normal;
        }
    }
}
