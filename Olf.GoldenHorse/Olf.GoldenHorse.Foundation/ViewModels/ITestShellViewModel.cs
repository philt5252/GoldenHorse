

namespace Olf.GoldenHorse.Foundation.ViewModels
{
    public interface ITestShellViewModel
    {
        ITestOperationsViewModel TestOperationsViewModel { get; }
        ITestDetailsViewModel TestDetailsViewModel { get; }
    }
}