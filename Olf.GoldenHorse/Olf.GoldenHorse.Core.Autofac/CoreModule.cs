using Autofac;
using Olf.GoldenHorse.Core.Controllers;
using Olf.GoldenHorse.Core.Factories.ViewModels;
using Olf.GoldenHorse.Core.ViewModels;
using Olf.GoldenHorse.Foundation.Controllers;
using Olf.GoldenHorse.Foundation.Factories.ViewModels;
using Olf.GoldenHorse.Foundation.ViewModels;

namespace Olf.GoldenHorse.Core.Autofac
{
    public class CoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<AppController>().As<IAppController>().SingleInstance();
            builder.RegisterType<ProjectController>().As<IProjectController>().SingleInstance();

            builder.RegisterType<MainShellViewModel>().As<IMainShellViewModel>();
            builder.RegisterType<NewProjectViewModel>().As<INewProjectViewModel>();

            builder.RegisterType<MainShellViewModelFactory>().As<IMainShellViewModelFactory>().SingleInstance();
            builder.RegisterType<NewProjectViewModelFactory>().As<INewProjectViewModelFactory>().SingleInstance();
        }
    }
}