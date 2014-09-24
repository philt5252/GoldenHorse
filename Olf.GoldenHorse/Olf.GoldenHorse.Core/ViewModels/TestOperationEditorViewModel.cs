
using Olf.GoldenHorse.Core.Models;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.ViewModels;

namespace Olf.GoldenHorse.Core.ViewModels
{
    public class TestOperationEditorViewModel : ITestOperationEditorViewModel
    {
        private readonly TestItem testItem;
        private string selectedOperation;

        public string[] Operations { get; protected set; }

        public string SelectedOperation
        {
            get
            {
                return selectedOperation;
            }
            set
            {
                if (Equals(selectedOperation, value))
                    return;

                selectedOperation = value;

                if (Equals(selectedOperation,"Left Click"))
                {
                    testItem.Operation = new LeftClickOperation();
                }
                else if (Equals(selectedOperation, "Right Click"))
                {
                    testItem.Operation = new RightClickOperation();
                }
                else if (Equals(selectedOperation, "Keyboard"))
                {
                    testItem.Operation = new KeyboardOperation();
                }
            }
        }

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