
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.ViewModels;

namespace Olf.GoldenHorse.Core.ViewModels
{
    public class TestDescriptionEditorViewModel : ITestDescriptionEditorViewModel
    {
        private readonly TestItem testItem;

        public string Description
        {
            get
            {
                return testItem.Description;
            }
            set
            {
                testItem.Description = value;
            }
        }

        public TestDescriptionEditorViewModel(TestItem testItem)
        {
            this.testItem = testItem;
        }
    }
}