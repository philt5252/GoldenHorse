using Autofac;
using Olf.GoldenHorse.Core.Controllers;
using Olf.GoldenHorse.Core.Factories.Services;
using Olf.GoldenHorse.Core.Factories.ViewModels;
using Olf.GoldenHorse.Core.Services;
using Olf.GoldenHorse.Core.ViewModels;
using Olf.GoldenHorse.Foundation.Controllers;
using Olf.GoldenHorse.Foundation.Factories.Services;
using Olf.GoldenHorse.Foundation.Factories.ViewModels;
using Olf.GoldenHorse.Foundation.Services;
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
            builder.RegisterType<ProjectSuiteController>().As<IProjectSuiteController>().SingleInstance();
            builder.RegisterType<RecordingController>().As<IRecordingController>().SingleInstance();

            builder.RegisterType<Recorder>().As<IRecorder>();
            builder.RegisterType<Camera>().As<ICamera>().SingleInstance();
            builder.RegisterType<ExternalAppInfoManager>().As<IExternalAppInfoManager>().SingleInstance();

            builder.RegisterType<RecorderFactory>().As<IRecorderFactory>().SingleInstance();

            builder.RegisterType<MainShellViewModel>().As<IMainShellViewModel>();
            builder.RegisterType<NewProjectSuiteViewModel>().As<INewProjectSuiteViewModel>();
            builder.RegisterType<TestMainShellViewModel>().As<ITestMainShellViewModel>();
            builder.RegisterType<TestShellViewModel>().As<ITestShellViewModel>();
            builder.RegisterType<TestScreenshotsViewModel>().As<ITestScreenshotsViewModel>();
            builder.RegisterType<TestOperationsViewModel>().As<ITestOperationsViewModel>();
            builder.RegisterType<TestDetailsViewModel>().As<ITestDetailsViewModel>();
            builder.RegisterType<ProjectExplorerViewModel>().As<IProjectExplorerViewModel>();
            builder.RegisterType<RecorderViewModel>().As<IRecorderViewModel>();

            builder.RegisterType<MainShellViewModelFactory>().As<IMainShellViewModelFactory>().SingleInstance();
            builder.RegisterType<NewProjectSuiteSuiteViewModelFactory>().As<INewProjectSuiteViewModelFactory>().SingleInstance();
            builder.RegisterType<TestMainShellViewModelFactory>().As<ITestMainShellViewModelFactory>().SingleInstance();
            builder.RegisterType<TestShellViewModelFactory>().As<ITestShellViewModelFactory>().SingleInstance();
            builder.RegisterType<TestScreenshotsViewModelFactory>().As<ITestScreenshotsViewModelFactory>().SingleInstance();
            builder.RegisterType<TestOperationsViewModelFactory>().As<ITestOperationsViewModelFactory>().SingleInstance();
            builder.RegisterType<TestDetailsViewModelFactory>().As<ITestDetailsViewModelFactory>().SingleInstance();
            builder.RegisterType<ProjectExplorerViewModelFactory>().As<IProjectExplorerViewModelFactory>().SingleInstance();
            builder.RegisterType<RecorderViewModelFactory>().As<IRecorderViewModelFactory>().SingleInstance();

        }
    }
}