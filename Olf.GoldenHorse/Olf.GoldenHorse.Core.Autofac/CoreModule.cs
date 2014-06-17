using Autofac;
using Olf.GoldenHorse.Core.Controllers;
using Olf.GoldenHorse.Core.Factories.Services;
using Olf.GoldenHorse.Core.Factories.ViewModels;
using Olf.GoldenHorse.Core.Factories.ViewModels.Nodes;
using Olf.GoldenHorse.Core.Services;
using Olf.GoldenHorse.Core.ViewModels;
using Olf.GoldenHorse.Core.ViewModels.Nodes;
using Olf.GoldenHorse.Foundation.Controllers;
using Olf.GoldenHorse.Foundation.Factories.Services;
using Olf.GoldenHorse.Foundation.Factories.ViewModels;
using Olf.GoldenHorse.Foundation.Factories.ViewModels.Nodes;
using Olf.GoldenHorse.Foundation.Models;
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
            builder.RegisterType<TestController>().As<ITestController>().SingleInstance();
            builder.RegisterType<LogController>().As<ILogController>().SingleInstance();
            builder.RegisterType<TestItemController>().As<ITestItemController>().SingleInstance();
            builder.RegisterType<VariableController>().As<IVariableController>().SingleInstance();

            builder.RegisterType<Recorder>().As<IRecorder>();

            builder.RegisterType<RecorderFactory>().As<IRecorderFactory>().SingleInstance();

            builder.RegisterType<MainShellViewModel>().As<IMainShellViewModel>();
            builder.RegisterType<NewProjectSuiteViewModel>().As<INewProjectSuiteViewModel>();
            builder.RegisterType<TestMainShellViewModel>().As<ITestMainShellViewModel>();
            builder.RegisterType<TestShellViewModel>().As<ITestShellViewModel>();
            builder.RegisterType<TestScreenshotsViewModel>().As<ITestScreenshotsViewModel>();
            builder.RegisterType<TestOperationsViewModel>().As<ITestOperationsViewModel>();
            builder.RegisterType<TestDetailsViewModel>().As<ITestDetailsViewModel>();
            builder.RegisterType<LogMainShellViewModel>().As<ILogMainShellViewModel>();
            builder.RegisterType<LogShellViewModel>().As<ILogShellViewModel>();
            builder.RegisterType<LogScreenshotsViewModel>().As<ILogScreenshotsViewModel>();
            builder.RegisterType<LogDetailsViewModel>().As<ILogDetailsViewModel>();
            builder.RegisterType<ProjectExplorerViewModel>().As<IProjectExplorerViewModel>();
            builder.RegisterType<RecorderViewModel>().As<IRecorderViewModel>();
            builder.RegisterType<TestItemViewModel>().As<ITestItemViewModel>();
            builder.RegisterType<LogItemViewModel>().As<ILogItemViewModel>();
            builder.RegisterType<StartPageViewModel>().As<IStartPageViewModel>();
            builder.RegisterType<RecentFileViewModel>().As<IRecentFileViewModel>();
            builder.RegisterType<TestItemEditorViewModel>().As<ITestItemEditorViewModel>();
            builder.RegisterType<TestObjectEditorViewModel>().As<ITestObjectEditorViewModel>();
            builder.RegisterType<TestOperationEditorViewModel>().As<ITestOperationEditorViewModel>();
            builder.RegisterType<TestParameterEditorViewModel>().As<ITestParameterEditorViewModel>();
            builder.RegisterType<TestDescriptionEditorViewModel>().As<ITestDescriptionEditorViewModel>();
            builder.RegisterType<EditParameterViewModel>().As<IEditParameterViewModel>();
            builder.RegisterType<ValidateTextAtPointListItemViewModel>().As<IValidationListItemViewModel>().SingleInstance();
            builder.RegisterType<GetObjectScreenSelectionViewModel>().As<IGetObjectScreenSelectionViewModel>();
            builder.RegisterType<OperationParameterViewModel>().As<IOperationParameterViewModel>();
            builder.RegisterType<VariableManagerViewModel>().As<IVariableManagerViewModel>();
            builder.RegisterType<VariableTableEditViewModel>().As<IVariableTableEditViewModel>();

            builder.RegisterType<TestNode>().AsSelf();
            builder.RegisterType<TestGroupNode>().AsSelf();
            builder.RegisterType<ProjectNode>().AsSelf();
            builder.RegisterType<ProjectLogsNode>().AsSelf();
            builder.RegisterType<LogNode>().AsSelf();
            builder.RegisterType<ProjectSuiteLogsNode>().AsSelf();
            builder.RegisterType<ProjectSuiteProjectsNode>().AsSelf();

            builder.RegisterType<MainShellViewModelFactory>().As<IMainShellViewModelFactory>().SingleInstance();
            builder.RegisterType<NewProjectSuiteSuiteViewModelFactory>().As<INewProjectSuiteViewModelFactory>().SingleInstance();
            builder.RegisterType<TestMainShellViewModelFactory>().As<ITestMainShellViewModelFactory>().SingleInstance();
            builder.RegisterType<TestShellViewModelFactory>().As<ITestShellViewModelFactory>().SingleInstance();
            builder.RegisterType<TestScreenshotsViewModelFactory>().As<ITestScreenshotsViewModelFactory>().SingleInstance();
            builder.RegisterType<TestOperationsViewModelFactory>().As<ITestOperationsViewModelFactory>().SingleInstance();
            builder.RegisterType<TestDetailsViewModelFactory>().As<ITestDetailsViewModelFactory>().SingleInstance();
            builder.RegisterType<LogMainShellViewModelFactory>().As<ILogMainShellViewModelFactory>().SingleInstance();
            builder.RegisterType<LogShellViewModelFactory>().As<ILogShellViewModelFactory>().SingleInstance();
            builder.RegisterType<LogScreenshotsViewModelFactory>().As<ILogScreenshotsViewModelFactory>().SingleInstance();
            builder.RegisterType<LogDetailsViewModelFactory>().As<ILogDetailsViewModelFactory>().SingleInstance();
            builder.RegisterType<ProjectExplorerViewModelFactory>().As<IProjectExplorerViewModelFactory>().SingleInstance();
            builder.RegisterType<RecorderViewModelFactory>().As<IRecorderViewModelFactory>().SingleInstance();
            builder.RegisterType<TestItemViewModelFactory>().As<ITestItemViewModelFactory>().SingleInstance();
            builder.RegisterType<LogItemViewModelFactory>().As<ILogItemViewModelFactory>().SingleInstance();
            builder.RegisterType<StartPageViewModelFactory>().As<IStartPageViewModelFactory>().SingleInstance();
            builder.RegisterType<RecentFileViewModelFactory>().As<IRecentFileViewModelFactory>().SingleInstance();
            builder.RegisterType<TestItemEditorViewModelFactory>().As<ITestItemEditorViewModelFactory>().SingleInstance();
            builder.RegisterType<TestObjectEditorViewModelFactory>().As<ITestObjectEditorViewModelFactory>().SingleInstance();
            builder.RegisterType<TestOperationEditorViewModelFactory>().As<ITestOperationEditorViewModelFactory>().SingleInstance();
            builder.RegisterType<TestParameterEditorViewModelFactory>().As<ITestParameterEditorViewModelFactory>().SingleInstance();
            builder.RegisterType<TestDescriptionEditorViewModelFactory>().As<ITestDescriptionEditorViewModelFactory>().SingleInstance();
            builder.RegisterType<EditParameterViewModelFactory>().As<IEditParameterViewModelFactory>().SingleInstance();
            builder.RegisterType<GetObjectScreenSelectionViewModelFactory>().As<IGetObjectScreenSelectionViewModelFactory>().SingleInstance();
            builder.RegisterType<OperationParameterViewModelFactory>().As<IOperationParameterViewModelFactory>().SingleInstance();
            builder.RegisterType<VariableManagerViewModelFactory>().As<IVariableManagerViewModelFactory>().SingleInstance();
            builder.RegisterType<VariableTableEditViewModelFactory>().As<IVariableTableEditViewModelFactory>().SingleInstance();

            builder.RegisterType<TestNodeFactory>().As<ITestNodeFactory>().SingleInstance();
            builder.RegisterType<TestGroupNodeFactory>().As<ITestGroupNodeFactory>().SingleInstance();
            builder.RegisterType<ProjectNodeFactory>().As<IProjectNodeFactory>().SingleInstance();
            builder.RegisterType<ProjectLogsNodeFactory>().As<IProjectLogsNodeFactory>().SingleInstance();
            builder.RegisterType<LogNodeFactory>().As<ILogNodeFactory>().SingleInstance();
            builder.RegisterType<ProjectSuiteLogsNodeFactory>().As<IProjectSuiteLogsNodeFactory>().SingleInstance();
            builder.RegisterType<ProjectSuiteProjectsNodeFactory>().As<IProjectSuiteProjectsNodeFactory>().SingleInstance();


            builder.RegisterType<OnScreenActionOperationViewModel>().As<IOperationViewModel>();
            builder.RegisterType<DelayOperationViewModel>().As<IOperationViewModel>();
            builder.RegisterType<LogMessageOperationViewModel>().As<IOperationViewModel>();
        }
    }
}