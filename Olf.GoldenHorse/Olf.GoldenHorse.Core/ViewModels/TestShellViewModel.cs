
using Olf.GoldenHorse.Foundation.ViewModels;

namespace Olf.GoldenHorse.Core.ViewModels
{
    public class TestShellViewModel : ITestShellViewModel
    {
        public ITestOperationsViewModel TestOperationsViewModel { get; protected set; }

        public ITestDetailsViewModel TestDetailsViewModel { get; protected set; }
    }
}