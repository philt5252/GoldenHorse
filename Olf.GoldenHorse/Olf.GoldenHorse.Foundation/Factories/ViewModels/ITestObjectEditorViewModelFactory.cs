
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.ViewModels;

namespace Olf.GoldenHorse.Foundation.Factories.ViewModels
{
    public interface ITestObjectEditorViewModelFactory
    {
        ITestObjectEditorViewModel Create(TestItem testItem);
    }
}