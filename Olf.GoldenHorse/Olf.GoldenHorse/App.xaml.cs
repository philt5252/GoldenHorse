using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using Autofac;
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
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ContainerBuilder builder = new ContainerBuilder();

            builder.RegisterModule<CoreModule>();
            builder.RegisterModule<ViewsModule>();
            builder.RegisterModule<DataAccessModule>();
            builder.RegisterModule<PrismModule>();

            IContainer container = builder.Build();

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
    }
}
