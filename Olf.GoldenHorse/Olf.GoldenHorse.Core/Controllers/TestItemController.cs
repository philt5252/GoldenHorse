using Olf.GoldenHorse.Foundation.Controllers;
using Olf.GoldenHorse.Foundation.Factories.ViewModels;
using Olf.GoldenHorse.Foundation.Models;
using Olf.GoldenHorse.Foundation.ViewModels;
using Olf.GoldenHorse.Foundation.Views;
using Olf.GoldenHorse.Foundation.Views.Factories;

namespace Olf.GoldenHorse.Core.Controllers
{
    public class TestItemController : ITestItemController
    {
        private readonly ITestItemEditorWindowFactory testItemEditorWindowFactory;
        private readonly ITestItemEditorViewModelFactory testItemEditorViewModelFactory;
        private IWindow testItemEditorWindow;

        public TestItem CurrentTestItem { get; protected set; }

        public TestItemController(ITestItemEditorWindowFactory testItemEditorWindowFactory,
            ITestItemEditorViewModelFactory testItemEditorViewModelFactory)
        {
            this.testItemEditorWindowFactory = testItemEditorWindowFactory;
            this.testItemEditorViewModelFactory = testItemEditorViewModelFactory;
        }

        public void EditTestItem(TestItem testItem)
        {
            CurrentTestItem = testItem;
            testItemEditorWindow = testItemEditorWindowFactory.Create();
            ITestItemEditorViewModel testItemEditorViewModel = testItemEditorViewModelFactory.Create(testItem);

            testItemEditorWindow.DataContext = testItemEditorViewModel;

            testItemEditorWindow.Show();
        }

        public void MinimizeTestItemEditorWindow()
        {
            testItemEditorWindow.Minimize();
        }

        public void RestoreTestItemEditorWindow()
        {
            testItemEditorWindow.Restore();
        }
    }
}