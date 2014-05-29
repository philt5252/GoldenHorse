

namespace Olf.GoldenHorse.Foundation.ViewModels
{
    public interface ITestMainShellViewModel
    {
        ITestScreenshotsViewModel TestScreenshotsViewModel { get; }
        ITestShellViewModel TestShellViewModel { get; }
    }
}