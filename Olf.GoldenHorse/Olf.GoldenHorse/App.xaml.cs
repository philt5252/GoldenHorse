using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using Autofac;
using AvalonDock.Layout;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.Regions.Behaviors;
using Microsoft.Practices.ServiceLocation;
using Olf.GoldenHorse.Core.Autofac;
using Olf.GoldenHorse.Core.DataAccess.Autofac;
using Olf.GoldenHorse.Core.Views.Autofac;
using Olf.GoldenHorse.Foundation.Controllers;
using Olf.Prism.Autofac;

namespace Olf.GoldenHorse
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IContainer container;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Application.Current.DispatcherUnhandledException +=
                (sender, args) =>
                {
                    MessageBox.Show(args.Exception.Message, "Error");
                };

            ContainerBuilder builder = new ContainerBuilder();

            builder.RegisterModule<CoreModule>();
            builder.RegisterModule<ViewsModule>();
            builder.RegisterModule<DataAccessModule>();
            builder.RegisterModule<PrismModule>();

            builder.RegisterType<LayoutAnchorablePaneRegionAdapter>().AsSelf();
            builder.RegisterType<LayoutDocumentPaneRegionAdapter>().AsSelf();
            container = builder.Build();

            ConfigureDefaultRegionBehaviors();
            ConfigureRegionAdapterMappings();

            container.RegisterAllFuncsAsOwned();

            IServiceLocator serviceLocator = container.Resolve<IServiceLocator>();

            ServiceLocator.SetLocatorProvider(() => serviceLocator);

            IAppController appController;

            using (ILifetimeScope scope = container.BeginLifetimeScope())
            {
                appController = scope.Resolve<IAppController>();
            }

            appController.Home();
        }

        protected virtual RegionAdapterMappings ConfigureRegionAdapterMappings()
        {
            var regionAdapterMappings = container.ResolveOptional<RegionAdapterMappings>();

            if (regionAdapterMappings != null)
            {
                regionAdapterMappings.RegisterMapping(typeof(Selector), container.Resolve<SelectorRegionAdapter>());
                regionAdapterMappings.RegisterMapping(typeof(ItemsControl), container.Resolve<ItemsControlRegionAdapter>());
                regionAdapterMappings.RegisterMapping(typeof(ContentControl), container.Resolve<ContentControlRegionAdapter>());
                regionAdapterMappings.RegisterMapping(typeof(LayoutAnchorablePane), container.Resolve<LayoutAnchorablePaneRegionAdapter>());
                regionAdapterMappings.RegisterMapping(typeof(LayoutDocumentPane), container.Resolve<LayoutDocumentPaneRegionAdapter>());
            }

            return regionAdapterMappings;
        }

        private void ConfigureDefaultRegionBehaviors()
        {
            IRegionBehaviorFactory defaultRegionBehaviorTypesDictionary;
            container.TryResolve(out defaultRegionBehaviorTypesDictionary);

            if (defaultRegionBehaviorTypesDictionary != null)
            {
                defaultRegionBehaviorTypesDictionary.AddIfMissing(AutoPopulateRegionBehavior.BehaviorKey,
                    typeof(AutoPopulateRegionBehavior));

                defaultRegionBehaviorTypesDictionary.AddIfMissing(BindRegionContextToDependencyObjectBehavior.BehaviorKey,
                    typeof(BindRegionContextToDependencyObjectBehavior));

                defaultRegionBehaviorTypesDictionary.AddIfMissing(RegionActiveAwareBehavior.BehaviorKey,
                    typeof(RegionActiveAwareBehavior));

                defaultRegionBehaviorTypesDictionary.AddIfMissing(SyncRegionContextWithHostBehavior.BehaviorKey,
                    typeof(SyncRegionContextWithHostBehavior));

                defaultRegionBehaviorTypesDictionary.AddIfMissing(RegionManagerRegistrationBehavior.BehaviorKey,
                    typeof(RegionManagerRegistrationBehavior));

            }
        }
    }
}
