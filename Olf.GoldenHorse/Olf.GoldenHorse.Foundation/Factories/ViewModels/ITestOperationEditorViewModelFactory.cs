
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.ViewModels;

namespace Olf.GoldenHorse.Foundation.Factories.ViewModels
{
    public interface ITestOperationEditorViewModelFactory
    {
        ITestOperationEditorViewModel Create(TestItem testItem);
    }
}