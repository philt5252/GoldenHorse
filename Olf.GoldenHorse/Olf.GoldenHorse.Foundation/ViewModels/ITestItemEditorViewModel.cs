

namespace Olf.GoldenHorse.Foundation.ViewModels
{
    public interface ITestItemEditorViewModel
    {
        ITestObjectEditorViewModel TestObjectEditorViewModel { get; }
        ITestOperationEditorViewModel TestOperationEditorViewModel { get; }
        ITestParameterEditorViewModel TestParameterEditorViewModel { get; }
        ITestDescriptionEditorViewModel TestDescriptionEditorViewModel { get; }
    }
}