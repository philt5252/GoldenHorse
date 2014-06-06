using System.Reflection;
using Autofac;
using Olf.GoldenHorse.Core.Views.Factories;
using Olf.GoldenHorse.Core.Views.Views;
using Olf.GoldenHorse.Core.Views.Views.Logs;
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
            builder.RegisterType<NewProjectSuiteWindow>().AsSelf();
            builder.RegisterType<TestShellView>().AsSelf();
            builder.RegisterType<RecordWindow>().AsSelf();
            builder.RegisterType<TestMainShellView>().AsSelf();
            builder.RegisterType<TestDetailsView>().AsSelf();
            builder.RegisterType<NewProjectWindow>().AsSelf();
            builder.RegisterType<ProjectExplorerView>().AsSelf();
            builder.RegisterType<ProjectWorkspaceView>().AsSelf();
            builder.RegisterType<LogMainShellView>().AsSelf();
            builder.RegisterType<LogShellView>().AsSelf();
            builder.RegisterType<StartView>().AsSelf();

            builder.RegisterType<MainWindowFactory>().As<IMainWindowFactory>().SingleInstance();
            builder.RegisterType<MainShellViewFactory>().As<IMainShellViewFactory>().SingleInstance();
            builder.RegisterType<NewProjectWindowFactory>().As<INewProjectWindowFactory>().SingleInstance();
            builder.RegisterType<RecordWindowFactory>().As<IRecordWindowFactory>().SingleInstance();
            builder.RegisterType<ProjectExplorerViewFactory>().As<IProjectExplorerViewFactory>().SingleInstance();
            builder.RegisterType<TestMainShellViewFactory>().As<ITestMainShellViewFactory>().SingleInstance();
            builder.RegisterType<NewProjectSuiteWindowFactory>().As<INewProjectSuiteWindowFactory>().SingleInstance();
            builder.RegisterType<ProjectWorkspaceViewFactory>().As<IProjectWorkspaceViewFactory>().SingleInstance();
            builder.RegisterType<TestShellViewFactory>().As<ITestShellViewFactory>().SingleInstance();
            builder.RegisterType<LogMainShellViewFactory>().As<ILogMainShellViewFactory>().SingleInstance();
            builder.RegisterType<LogShellViewFactory>().As<ILogShellViewFactory>().SingleInstance();
            builder.RegisterType<StartViewFactory>().As<IStartViewFactory>().SingleInstance();
        }
    }
}