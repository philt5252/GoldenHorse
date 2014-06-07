

using System.ComponentModel;
using System.Windows.Input;

namespace Olf.GoldenHorse.Foundation.ViewModels
{
    public interface ITestItemEditorViewModel : INotifyPropertyChanged
    {
        ITestObjectEditorViewModel TestObjectEditorViewModel { get; }
        ITestOperationEditorViewModel TestOperationEditorViewModel { get; }
        ITestParameterEditorViewModel TestParameterEditorViewModel { get; }
        ITestDescriptionEditorViewModel TestDescriptionEditorViewModel { get; }
        ICommand CancelCommand { get; }
        ICommand NextCommand { get; }
        ICommand FinishCommand { get; }
        int SelectedIndex { get; set; }
    }
}