using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using MahApps.Metro.Controls;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using Olf.GoldenHorse.Foundation.Views;

namespace Olf.GoldenHorse.Core.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : WindowBase
    {
        private IRegionManager regionManager;
        public MainWindow()
        {
         
            regionManager = ServiceLocator.Current.GetInstance<IRegionManager>();
            RegionManager.SetRegionManager(this, regionManager);
            
            
            InitializeComponent();

            RegionManager.UpdateRegions();

            Closing += OnClosing;
        }

        private void OnClosing(object sender, CancelEventArgs cancelEventArgs)
        {
            regionManager.Regions.Remove("MainShellViewRegion");
        }
    }
}
