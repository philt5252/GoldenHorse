

namespace Olf.GoldenHorse.Foundation.ViewModels
{
    public interface ITestItemEditorViewModel
    {
        ITestObjectEditorViewModel TestObjectEditorViewModel { get; }
        ITestOperationEditorViewModel TestOperationEditorViewModel { get; }
        ITestParameterEditorViewModel TestParameterEditorView { get; }
        ITestDescriptionEditorViewModel TestDescriptionEditorViewModel { get; }
    }
}