
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.ViewModels;

namespace Olf.GoldenHorse.Core.ViewModels
{
    public class TestOperationEditorViewModel : ITestOperationEditorViewModel
    {
        private readonly TestItem testItem;
        public string[] Operations { get; protected set; }

        public TestOperationEditorViewModel(TestItem testItem)
        {
            this.testItem = testItem;

            if (testItem.Type == TestItemTypes.OnScreenAction)
            {
                Operations = new[] {"Left Click", "Right Click", "Keyboard"};
            }
        }
    }
}