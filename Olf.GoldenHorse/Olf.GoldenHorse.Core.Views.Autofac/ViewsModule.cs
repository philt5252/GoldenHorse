using System.Reflection;
using Autofac;
using Olf.GoldenHorse.Core.Views.Factories;
using Olf.GoldenHorse.Foundation.Views.Factories;
using Module = Autofac.Module;


namespace Olf.GoldenHorse.Core.Views.Autofac
{
    public class ViewsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<MainWindow>().AsSelf();
            builder.RegisterType<MainShellView>().AsSelf();
            builder.RegisterType<NewProjectWindow>().AsSelf();
            builder.RegisterType<WorkspaceView>().AsSelf();

            builder.RegisterType<MainWindowFactory>().As<IMainWindowFactory>().SingleInstance();
            builder.RegisterType<MainShellViewFactory>().As<IMainShellViewFactory>().SingleInstance();
            builder.RegisterType<NewProjectWindowFactory>().As<INewProjectWindowFactory>().SingleInstance();
            builder.RegisterType<WorkspaceViewFactory>().As<IWorkspaceViewFactory>().SingleInstance();
        }
    }
}