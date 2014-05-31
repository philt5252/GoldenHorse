

using Olf.GoldenHorse.Foundation.Annotations;

namespace Olf.GoldenHorse.Foundation.ViewModels
{
    public interface ITestMainShellViewModel
    {
        string TestName { get; }
        ITestScreenshotsViewModel TestScreenshotsViewModel { get; }
        ITestShellViewModel TestShellViewModel { get; }
    }
}