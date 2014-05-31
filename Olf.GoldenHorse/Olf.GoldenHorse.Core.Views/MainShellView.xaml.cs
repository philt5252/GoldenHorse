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
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using Olf.GoldenHorse.Foundation.Views;
using Xceed.Wpf.AvalonDock.Layout;

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
    }
}
