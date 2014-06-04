using Olf.GoldenHorse.Foundation.Models;

namespace Olf.GoldenHorse.Foundation.ViewModels
{
    public interface ITestMainShellViewModel
    {
        Test Test { get; }
        string TestName { get; }
        ITestScreenshotsViewModel TestScreenshotsViewModel { get; }
        ITestShellViewModel TestShellViewModel { get; }
    }
}