
using Olf.GoldenHorse.Foundation.ViewModels;

namespace Olf.GoldenHorse.Core.ViewModels
{
    public class TestOperationEditorViewModel : ITestOperationEditorViewModel
    {
        public string[] Operations { get; protected set; }

        public TestOperationEditorViewModel()
        {
            Operations = new[] {"one", "two", "three"};
        }
    }
}