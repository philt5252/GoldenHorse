﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Olf.GoldenHorse.Foundation.Views;

namespace Olf.GoldenHorse.Core.Views
{
    /// <summary>
    /// Interaction logic for WorkspaceView.xaml
    /// </summary>
    public partial class WorkspaceView : UserControl, IViewWithDataContext
    {
        public WorkspaceView()
        {
            InitializeComponent();

        }

        private void WorkspaceView_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            //TestMainShellViewModel testMainShellViewModel = sender as TestMainShellViewModel;
        }

       

        
    }
}
