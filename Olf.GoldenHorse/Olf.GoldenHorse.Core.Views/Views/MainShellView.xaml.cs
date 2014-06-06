using System;
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
using AvalonDock.Layout;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using Olf.GoldenHorse.Foundation.Views;

namespace Olf.GoldenHorse.Core.Views
{
    /// <summary>
    /// Interaction logic for MainShellView.xaml
    /// </summary>
    public partial class MainShellView : UserControl, IViewWithDataContext
    {
        public MainShellView()
        {
            InitializeComponent();

            RegionAdapterMappings regionAdapterMappings = ServiceLocator.Current.GetInstance<RegionAdapterMappings>();
            IRegionAdapter regionAdapter = regionAdapterMappings.GetMapping(typeof (LayoutDocumentPane));
            IRegion region = regionAdapter.Initialize(layoutDocumentPane, Regions.MainWorkspaceViewRegion);

            //IRegionManager regionManager = ServiceLocator.Current.GetInstance<IRegionManager>();
            //regionManager.Regions.Add(region);

            RegionManager.UpdateRegions();
        }

        private void ToolBar_Loaded(object sender, RoutedEventArgs e)
        {
            ToolBar toolBar = sender as ToolBar;
            var overflowGrid = toolBar.Template.FindName("OverflowGrid", toolBar) as FrameworkElement;
            if (overflowGrid != null)
            {
                overflowGrid.Visibility = Visibility.Collapsed;
            }
        }
    }
}
