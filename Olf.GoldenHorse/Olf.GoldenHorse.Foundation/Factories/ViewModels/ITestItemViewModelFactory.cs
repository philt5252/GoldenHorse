using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.ViewModels;

namespace Olf.GoldenHorse.Foundation.Factories.ViewModels
{
    public interface ITestItemViewModelFactory
    {
        ITestItemViewModel Create(TestItem testItem);
    }
}