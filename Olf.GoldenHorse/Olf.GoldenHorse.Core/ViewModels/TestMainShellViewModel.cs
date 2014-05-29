
using Olf.GoldenHorse.Foundation.ViewModels;

namespace Olf.GoldenHorse.Core.ViewModels
{
    public class TestMainShellViewModel : ITestMainShellViewModel
    {
        public ITestScreenshotsViewModel TestScreenshotsViewModel { get; protected set; }
        public ITestShellViewModel TestShellViewModel { get; protected set; }
    }
}